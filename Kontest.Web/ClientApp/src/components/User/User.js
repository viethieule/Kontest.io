import React, { Component } from 'react'
import { Button, Table } from 'reactstrap'
import { Link } from 'react-router-dom'
import userService from '../../services/UserService';
import userOrganizationService from '../../services/UserOrganizationService';

export class User extends Component {
    constructor(props) {
        super(props);
        this.state = {
            user: null
        }
    }

    componentDidMount() {
        this.populateUserData();
    }

    render() {
        const user = this.state.user;
        return !!user ?
            <div>
                <img alt={user.userName} src={user.profilePicture}></img>
                <h2>{user.userName} ({user.role})</h2>
                <div>
                    <Button color="primary">Thông tin tài khoản</Button>{' '}
                    <Button tag={Link} to='/organizationrequest/create' color="success">Đăng ký organizer</Button>{' '}
                    <Button color="info">Đăng xuất</Button>
                </div>
                <p>Danh sách organization</p>
                <OrganizationList userId={user.id} />
            </div> :
            <div></div>
    }

    async populateUserData() {
        const user = await userService.getUserById(this.props.id);
        this.setState({ user });
    }
}

class OrganizationList extends Component {
    state = {
        userOrganizations: []
    }

    componentDidMount() {
        this.populateOrganizationList();
    }

    render() {
        const { userOrganizations } = this.state;
        return (
            userOrganizations.length > 0 ?
                <Table>
                    <thead>
                        <tr>
                            <th>Organization Id</th>
                            <th>Organization Name</th>
                            <th>My role</th>
                            <th>Join date</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {userOrganizations.map((uo) =>
                            <UserOrganizationListRow key={uo.organization.id} userOrganization={uo} />
                        )}
                    </tbody>
                </Table> :
                <div></div>
        );
    }

    async populateOrganizationList() {
        const userOrganizations = await userOrganizationService.getUserOrganizationsByUserId(this.props.userId);
        this.setState({ userOrganizations });
    }
}

const UserOrganizationListRow = ({ userOrganization }) => {
    const org = userOrganization.organization;
    return (
        <tr>
            <td>{org.id}</td>
            <td>
                <Button
                    tag={Link}
                    to={{
                        pathname: `/organization/${org.alias}`,
                        orgId: org.id
                    }}
                    color="link">
                    {org.name}
                </Button>
            </td>
            <td>{userOrganization.orgnizationUserRoleType}</td>
            <td>{userOrganization.assignedDate}</td>
            <td>Leave</td>
        </tr>
    )
}

