import React, { Component } from 'react'
import authService from '../../services/AuthService'
import { Redirect } from 'react-router-dom'
import { ApplicationPaths } from '../../constants/Auth/AuthConstants';

export class Profile extends Component {
    constructor(props) {
        super(props);
        this.state = {
            user: undefined,
            loading: true
        }
    }

    componentDidMount() {
        this.populateUserData();
    }

    render() {
        const { user, loading } = this.state;    
        if (loading) {
            return <div></div>
        } else {
            if (!!user) {
                return <h1>{user.sub}</h1>
            }
            else {
                return <Redirect to={ApplicationPaths.Login} />
            }
        }
    }

    async populateUserData() {
        setTimeout(async () => {
            const user = await authService.getUser();
            if (!!user) {
                this.setState({ user: user, loading: false });
            }
            else {
                this.setState({ loading: false })
            }
        }, 2000);
    }
}