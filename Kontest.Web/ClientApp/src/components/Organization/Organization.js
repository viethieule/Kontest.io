import React, { Component, Fragment, useState, useEffect } from 'react';
import { Button, Table, FormGroup, Col, Label, Input, ModalBody, ModalFooter, ListGroup, ListGroupItem } from 'reactstrap'
import CommomModal from '../Modals/CommonModal';
import organizationService from '../../services/OrganizationService';
import userOrganizationService, { UserOrganizationsRoleType } from '../../services/UserOrganizationService';
import { debounce } from 'lodash/function';
import userService from '../../services/UserService';
import authService from '../../services/AuthService';
import { ConfirmModal } from '../Modals/ConfirmModal';

export class Organization extends Component {
    constructor(props) {
        super(props);
        this.state = {
            organization: null,
            userOrganizations: [],
            toggleRefreshMemberList: false
        }

        this.onRecordAdded = this.onRecordAdded.bind(this);
        this.onUserOrgDeleted = this.onUserOrgDeleted.bind(this);
    }

    componentDidMount() {
        this.populateOrganizationInfo();
        this.populateMemberList();
    }

    async onRecordAdded(userId, roleType) {
        alert(`Adding ${userId} with role ${roleType} to organization ${this.state.organization.id}`);
        const orgId = this.state.organization.id;
        const existingUserOrganizations = this.state.userOrganizations
            .filter(x => x.userId === userId && x.organizationId === orgId && !!x.orgnizationUserRoleType);

        if (existingUserOrganizations.some(x => x.orgnizationUserRoleType.value === parseInt(roleType))) {
            alert(`User đã có role trong organization!`);
        }
        else if (existingUserOrganizations.length > 0) {
            alert('Hủy role trước khi assign role mới!')
        }
        else {
            const addedUserOrg = await userOrganizationService.addUserOrganization({
                userId, orgnizationUserRoleType: parseInt(roleType), organizationId: orgId
            });

            if (!!addedUserOrg) {
                const userOrganizations = [...this.state.userOrganizations, addedUserOrg];
                this.setState({ userOrganizations });
            } else {
                alert('Đã có lỗi khi assign')
            }
        }
    }

    async onUserOrgDeleted(uoId) {
        const userOrganizations = this.state.userOrganizations.filter(x => x.id !== uoId);
        this.setState({ userOrganizations });
    }

    render() {
        const { organization, userOrganizations } = this.state;
        return !!organization ?
            <div>
                <img alt={organization.name} src={organization.profilePicture}></img>
                <h2>{organization.name}</h2>
                <div>
                    <Button color="info">Thông tin organizer</Button>{' '}
                    <CommomModal
                        triggerButtonClass="primary"
                        triggerButtonLabel="Add new member"
                        title="Assign new role"
                    >
                        <AssignNewRoleModal onRecordAdded={this.onRecordAdded} />
                    </CommomModal>
                </div>
                <p>Danh sách member</p>
                <MemberList userOrganizations={userOrganizations} onUserOrgDeleted={this.onUserOrgDeleted}></MemberList>
            </div> :
            <div></div>
    }

    async populateOrganizationInfo() {
        const orgId = this.props.location.orgId || this.props.orgId;
        const organization = await organizationService.getOrganizationById(orgId);
        this.setState({ organization });
    }

    async populateMemberList() {
        const orgId = this.props.location.orgId || this.props.orgId;
        const userOrganizations = await userOrganizationService.getUserOrganizationsByOrganizationId(orgId);
        this.setState({ userOrganizations });
    }
}

const MemberList = (props) => {
    const { userOrganizations } = props;
    const [currentUserRole, setCurrentUserRole] = useState(null);

    useEffect(() => {
        const getCurrentUserRoleInOrg = async () => {
            const currentUser = await authService.getUser();
            if (!!currentUser) {
                const userOrganization = userOrganizations.find(x => x.userId === currentUser.sub);
                if (!!userOrganization) {
                    setCurrentUserRole(userOrganization.orgnizationUserRoleType.value);
                }
            }
        }

        getCurrentUserRoleInOrg();
    }, [userOrganizations]);

    return !!userOrganizations ?
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
                {userOrganizations.map(uo =>
                    <MemberListRow
                        key={uo.userId}
                        currentUserRole={currentUserRole}
                        onUserOrgDeleted={props.onUserOrgDeleted}
                        userOrganization={uo}
                    />
                )}
            </tbody>
        </Table> :
        <div></div>
}

const MemberListRow = (props) => {
    const { currentUserRole, onUserOrgDeleted } = props;
    const { id, user, orgnizationUserRoleType, assignedDate, assignedBy } = props.userOrganization;
    const role = orgnizationUserRoleType.value;
    const deleteUserOrg = async (id) => {
        const ok = await userOrganizationService.deleteUserOrganizationById(id);
        if (ok) {
            onUserOrgDeleted(id);
        } else {
            alert('Hủy đéo được');
        }
    }

    let deleteRoleBtn = (
        <CommomModal
            triggerButtonClass="danger"
            triggerButtonLabel="Hủy role"
            title="Leave organization"
            callBack={() => deleteUserOrg(id)}
        >
            <ConfirmModal message={`Hủy role của ${user.username} ?`} />
        </CommomModal>
    );

    if (currentUserRole === UserOrganizationsRoleType.CREATOR && role === UserOrganizationsRoleType.CREATOR) {
        deleteRoleBtn = null;
    } else if (currentUserRole === UserOrganizationsRoleType.ADMIN && role !== UserOrganizationsRoleType.MEMBER) {
        deleteRoleBtn = null
    } else if (currentUserRole === UserOrganizationsRoleType.MEMBER) {
        deleteRoleBtn = null;
    }

    return (
        <tr>
            <td>{user.username}</td>
            <td>{!!orgnizationUserRoleType ? orgnizationUserRoleType.name : ''}</td>
            <td>{assignedDate}</td>
            <td>{assignedBy}</td>
            <td>{deleteRoleBtn}</td>
        </tr>
    );
}

class AssignNewRoleModal extends Component {
    constructor(props) {
        super(props);
        this.state = {
            fields: {
                userId: '',
                roleType: ''
            },
            roles: []
        }

        this.handleOnUserSelected = this.handleOnUserSelected.bind(this)
        this.handleOnChange = this.handleOnChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleSubmit() {
        const { userId, roleType } = this.state.fields;
        this.props.onRecordAdded(userId, roleType);
        this.props.proceed();
    }

    handleOnUserSelected(userId) {
        this.setState(prevState => ({
            ...prevState,
            fields: {
                ...prevState.fields,
                userId
            }
        }));
    }

    handleOnChange(event) {
        const { name, value } = event.target;

        this.setState(prevState => ({
            ...prevState,
            fields: {
                ...prevState.fields,
                [name]: value
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
                            <SearchBox onRecordSelected={this.handleOnUserSelected}></SearchBox>{/* TODO: HLV - should pass userId / keyword as props, this makes userId state redundant */}
                        </Col>
                    </FormGroup>
                    <FormGroup row>
                        <Label for="role" sm={3}>Role</Label>
                        <Col sm={9}>
                            <Input type="select" name="roleType" value={this.state.fields.roleType} onChange={this.handleOnChange}>
                                <option value={UserOrganizationsRoleType.ADMIN}>Admin</option>
                                <option value={UserOrganizationsRoleType.MEMBER}>Member</option>
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
}

// TODO: Refactor to use as common func
class SearchBox extends Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            keyword: '',
            listOptions: [],
            isShowListOptions: false
        }
    }

    displaySearchResults = debounce(async keyword => {
        let users = await userService.searchUserByUsername(keyword);
        users = users.map(user => ({
            id: user.id,
            name: user.username
        }));
        this.setState({ listOptions: users, loading: false });
    }, 500) // TODO: save as const

    handleOnKeyUp(event) {
        const keyword = event.target.value.trim();
        if (!keyword) {
            this.setState({ isShowListOptions: false, loading: false })
            return;
        }

        this.setState({ isShowListOptions: true, loading: true });
        this.displaySearchResults(keyword);
    }

    fillInput(id) {
        const { listOptions } = this.state;
        const record = listOptions.find(x => x.id === id);
        this.setState({ keyword: record.name, loading: false, isShowListOptions: false });
        this.props.onRecordSelected(id);
    }

    handleOnChange(event) {
        this.setState({ keyword: event.target.value.trim() });
    }

    render() {
        return (
            <Fragment>
                <Input
                    type="text"
                    name="search"
                    value={this.state.keyword}
                    onKeyUp={this.handleOnKeyUp.bind(this)}
                    onChange={this.handleOnChange.bind(this)} // onChange behave like onInput here. TODO: can replace onKeyUp with onCHange ?
                />
                {
                    this.state.isShowListOptions ?
                        <ListGroup>
                            {
                                this.state.loading ?
                                    <ListGroupItem>Loading...</ListGroupItem> :
                                    this.state.listOptions.map(option =>
                                        <ListGroupItem
                                            key={option.id}
                                            tag="button"
                                            onClick={this.fillInput.bind(this, option.id)}
                                            action
                                        >
                                            {option.name}
                                        </ListGroupItem>
                                    )
                            }
                        </ListGroup> :
                        null
                }
            </Fragment >
        )
    }
}