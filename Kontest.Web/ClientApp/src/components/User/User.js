import React, { Component } from 'react'
import { Button, Table } from 'reactstrap'
import { Link } from 'react-router-dom'

export class User extends Component {
    constructor(props) {
        super(props);
        this.state = {
            user: null
        }
    }

    componentDidMount() {
        let user = this.fakeFetchUser(this.props.id);
        this.setState({ user });
    }

    render() {
        const user = this.state.user;
        return !!user ?
            <div>
                <img alt={user.Username} src={user.ProfilePicture}></img>
                <h2>{user.Username} ({user.Role})</h2>
                <div>
                    <Button color="primary">Thông tin tài khoản</Button>{' '}
                    <Button tag={Link} to='/organizationrequest/create' color="success">Đăng ký organizer</Button>{' '}
                    <Button color="info">Đăng xuất</Button>
                </div>
                <p>Danh sách organization</p>
                <OrganizationList userId={user.Id} />
            </div> :
            <div></div>
    }

    fakeFetchUser = (id) => {
        if (!!id) {
            return {
                Id: id,
                ProfilePicture: 'https://picsum.photos/200',
                Username: 'viethieule',
                Fullname: 'Viet Hieu Le',
                Role: 'user'
            }
        }

        return null;
    }
}

class OrganizationList extends Component {
    render() {
        const organizations = this.fakeGetOrganizationByUser(this.props.userId);
        return (
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
                    {organizations.map((org, i) =>
                        <OrganizationListRow key={i} org={org} />
                    )}
                </tbody>
            </Table>
        )
    }

    fakeGetOrganizationByUser(userId) {
        return FAKE_ORGANIZATION_LIST;
    }
}

const OrganizationListRow = (props) => {
    const org = props.org;
    return (
        <tr>
            <td>{org.Id}</td>
            <td>
                <Button
                    tag={Link}
                    to={{
                        pathname: `/organization/${org.Name}`,
                        orgId: org.Id
                    }}
                    color="link">{org.Name}
                </Button>
            </td>
            <td>{org.Role}</td>
            <td>{
                [org.JoinDate.getDay(), org.JoinDate.getMonth(), org.JoinDate.getFullYear()].join('.')
            }</td>
            <td>{!!org.Action ? org.Action : ''}</td>
        </tr>
    )
}

export const FAKE_ORGANIZATION_LIST = [
    {
        Id: 1001,
        Name: 'CLB Tin học UEH',
        Role: 'Creator',
        JoinDate: new Date(),
        Action: null,
        ProfilePicture: 'https://picsum.photos/201'
    },
    {
        Id: 1002,
        Name: 'CLB Tình chái UEH',
        Role: 'Admin',
        JoinDate: new Date(),
        Action: 'Leave',
        ProfilePicture: 'https://picsum.photos/202'
    },
    {
        Id: 1003,
        Name: 'CLB Như sận UEH',
        Role: 'Member',
        JoinDate: new Date(),
        Action: 'Leave',
        ProfilePicture: 'https://picsum.photos/199'
    }
]

