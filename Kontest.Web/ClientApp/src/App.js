import React, { Component } from 'react'
import { Route } from 'react-router'
import { Layout } from './components/Layout/Layout'
import { Home } from './components/Home/Home'
import AuthRoute from './components/Auth/AuthRoute'
import { ApplicationPaths } from './constants/Auth/AuthConstants'
import { Counter } from './components/Counter'
import AuthActionRoutes from './components/Auth/AuthActionRoutes'

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <AuthRoute path='/counter' component={Counter} />
        <Route path={ApplicationPaths.AuthorizationPrefix} component={AuthActionRoutes} />
      </Layout>
    );
  }
}
