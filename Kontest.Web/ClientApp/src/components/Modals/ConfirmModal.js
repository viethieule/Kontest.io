import React, { Fragment } from 'react';
import { Button, ModalBody, ModalFooter } from 'reactstrap'

export const ConfirmModal = (props) => {
    const { message } = props;
    return (
        <Fragment>
            <ModalBody>
                <h5>{message}</h5>
            </ModalBody>
            <ModalFooter>
                <Button color="primary" onClick={() => props.proceed()}>Yah</Button>{' '}
                <Button color="secondary" onClick={() => props.toggle()}>Nope</Button>
            </ModalFooter>
        </Fragment>
    )
}