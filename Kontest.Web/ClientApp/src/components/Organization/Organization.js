import React, { Component, Fragment } from 'react';
import { Button, Table, FormGroup, Col, Label, Input } from 'reactstrap'
import CommomModal from '../Modals/CommonModal';
import { ModalBody, ModalFooter } from 'reactstrap';
import organizationService from '../../services/OrganizationService';
import userService from '../../services/UserService';

export class Organization extends Component {
    constructor(props) {
        super(props);
        this.state = {
            organization: null,
            toggleRefreshMemberList: false
        }
    }

    componentDidMount() {
        this.populateOrganizationInfo();
    }

    render() {
        const { organization } = this.state;
        return !!organization ?
            <div>
                <img alt={organization.name} src={organization.profilePicture}></img>
                <h2>{organization.name}</h2>
                <div>
                    <Button color="info">Thông tin organizer</Button>{' '}
                    <CommomModal
                        triggerButtonLabel="Add new member"
                        title="Assign new role"
                        callBack={() => this.setState({ toggleRefreshMemberList: !this.state.toggleRefreshMemberList })}>
                        <AssignNewRoleModal />
                    </CommomModal>
                </div>
                <p>Danh sách member</p>
                <MemberList orgId={this.props.location.orgId} refresh={this.state.toggleRefreshMemberList}></MemberList>
            </div> :
            <div></div>
    }

    async populateOrganizationInfo() {
        const orgId = this.props.location.orgId || this.props.orgId;
        const organization = await organizationService.getOrganizationById(orgId);
        this.setState({ organization });
    }
}

class MemberList extends Component {
    constructor(props) {
        super(props);
        this.state = {
            members: []
        }
    }

    componentDidUpdate(prevProps) {
        if (this.props.refresh !== prevProps.refresh) {
            this.populateMemberList();
        }
    }

    componentDidMount() {
        this.populateMemberList();
    }

    render() {
        const { members } = this.state;
        return !!members ?
            <Table>
                <thead>
                    <tr>
                        <th>User</th>
                        <th>Role</th>
                        <th>Assigned on</th>
                        <th>Assigned by</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {members.map(member => <MemberListRow key={member.id} member={member} />)}
                </tbody>
            </Table> :
            <div></div>
    }

    async populateMemberList() {
        const { orgId } = this.props;
        const members = await userService.getMembersByOrganizationId(orgId);
        this.setState({ members });
    }
}

const MemberListRow = (props) => {
    const { member } = props;
    return (
        <tr>
            <td>{member.username}</td>
            <td></td>
            <td>{member.assignedOn}</td>
            <td>{member.assignedBy}</td>
            <td>Hủy role</td>
        </tr>
    );
}

class AssignNewRoleModal extends Component {
    constructor(props) {
        super(props);
        this.state = {
            fields: {
                user: {
                    name: '',
                    id: ''
                },
                role: ''
            },
            roles: []
        }

        this.handleUserOnChange = this.handleUserOnChange.bind(this);
        this.handleOnChange = this.handleOnChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount() {
        this.populateRole();
    }

    handleSubmit() {
        const { user, role } = this.state.fields;
        FAKE_MEMBER_LIST.push({
            Id: user.id,
            Username: user.name,
            Role: this.state.roles.find(r => r.id === parseInt(role)).name,
            AssignedOn: '16.12.2019', // format dd mm yyyy new Date()
            AssignedBy: 'Alice', // get current user here
        });
        this.props.proceed();
    }

    handleUserOnChange(event) {
        this.handleOnChange(event, value => {
            return {
                name: value,
                id: Math.floor(Math.random() * 10)
            }
        });
    }

    handleOnChange(event, handler) {
        const target = event.target;
        const name = target.name;
        const value = target.value;

        this.setState(prevState => ({
            ...prevState,
            fields: {
                ...prevState.fields,
                [name]: !!handler ? handler(value) : value
            }
        }));

        console.log(this.state);
    }

    render() {
        return (
            <Fragment>
                <ModalBody>
                    <h3>Vui lòng cung cấp các thông tin sau</h3>
                    <FormGroup row>
                        <Label for="user" sm={3}>Tên tài khoản</Label>
                        <Col sm={9}>
                            <Input type="text" name="user" value={this.state.fields.user.name} onChange={this.handleUserOnChange}></Input>
                        </Col>
                    </FormGroup>
                    <FormGroup row>
                        <Label for="role" sm={3}>Role</Label>
                        <Col sm={9}>
                            <Input type="select" name="role" value={this.state.fields.role} onChange={this.handleOnChange}>
                                {this.state.roles.map(role =>
                                    <option value={role.id}>{role.name}</option>
                                )}
                            </Input>
                        </Col>
                    </FormGroup>
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={this.handleSubmit}>Xác nhận</Button>{' '}
                    <Button color="secondary" onClick={this.props.toggle}>Hủy</Button>
                </ModalFooter>
            </Fragment>
        )
    }

    populateRole() {
        const roles = this.getAllRoles() || [];
        this.setState({ roles: roles });
        roles.length > 0 && this.setState(prevState => ({
            ...prevState,
            fields: {
                ...prevState.fields,
                role: roles[0].id
            }
        }));
    }

    getAllRoles() {
        // Exclude Creator role
        return [
            {
                id: 1,
                name: 'Admin'
            },
            {
                id: 2,
                name: 'Member'
            },
        ];
    }
}

var FAKE_MEMBER_LIST = [
    {
        Id: 1001,
        Username: 'viethieule',
        Role: 'Creator',
        AssignedOn: '2.2.2020',
        AssignedBy: 'System',
    },
    {
        Id: 1002,
        Username: 'davidngo',
        Role: 'Admin',
        AssignedOn: '2.3.2020',
        AssignedBy: 'viethieule',
    },
    {
        Id: 1003,
        Username: 'chotung',
        Role: 'User',
        AssignedOn: '2.4.2020',
        AssignedBy: 'davidngo',
    }
]