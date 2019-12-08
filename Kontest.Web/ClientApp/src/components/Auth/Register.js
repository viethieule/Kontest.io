import React, { Component } from 'react'
import { FormGroup, Col, Label, Input } from 'reactstrap'
import { Redirect } from 'react-router-dom'
import { ApplicationPaths } from '../../constants/Auth/AuthConstants'

export class Register extends Component {
    constructor(props) {
        super(props);
        this.state = {
            Username: '',
            Password: '',
            PasswordConfirm: '',
            Email: '',
            Otac: ''
        }

        this.handleOnChange = this.handleOnChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleOnChange(event) {
        const target = event.target;
        const name = target.name;
        const value = target.value;

        this.setState({ [name]: value });
    }

    async handleSubmit(event) {
        event.preventDefault();
        debugger;
        var response = await fetch('https://localhost:5200/api/account/register', {
            method: 'POST',
            mode: 'cors',
            credentials: 'same-origin',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify((({ Username, Password, Email }) => ({ Username, Password, Email }))(this.state))
        });

        const otac = await response.text();
        debugger;
        if (!!otac) {
            this.setState({ Otac: otac });
        }
    }

    render() {
        return (
            !!this.state.Otac ?
            <Redirect to={{
                pathname: ApplicationPaths.Login,
                state: { optionalAuthSettings: { acr_values: `otac:${this.state.Otac}` } } 
            }} /> :
            <div>
                <h1>Tạo tài khoản</h1>
                <form onSubmit={this.handleSubmit}>
                    <FormGroup row>
                        <Label for="Username" sm={2}>Tên tài khoản</Label>
                        <Col sm={10}>
                            <Input type="text" name="Username" value={this.state.Username} onChange={this.handleOnChange}></Input>
                        </Col>
                    </FormGroup>
                    <FormGroup row>
                        <Label for="Email" sm={2}>Email</Label>
                        <Col sm={10}>
                            <Input type="email" name="Email" value={this.state.Email} onChange={this.handleOnChange}></Input>
                        </Col>
                    </FormGroup>
                    <FormGroup row>
                        <Label for="Password" sm={2}>Mật khẩu</Label>
                        <Col sm={10}>
                            <Input type="password" name="Password" value={this.state.Password} onChange={this.handleOnChange}></Input>
                        </Col>
                    </FormGroup>
                    <FormGroup row>
                        <Label for="PasswordConfirm" sm={2}>Xác nhận mật khẩu</Label>
                        <Col sm={10}>
                            <Input type="password" name="PasswordConfirm" value={this.state.PasswordConfirm} onChange={this.handleOnChange}></Input>
                        </Col>
                    </FormGroup>
                    <Input type="submit" value="Submit"></Input>
                </form>
            </div>
        )
    }
}
