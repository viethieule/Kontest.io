import React, { useState, Fragment, Children, cloneElement } from 'react';
import { Button, Modal, ModalHeader } from 'reactstrap';

const CommomModal = (props) => {
    const { title, triggerButtonClass, triggerButtonLabel, className, callBack } = props;
    const [modal, setModal] = useState(false);

    const toggle = () => setModal(!modal);
    const proceed = () => {
        !!callBack && callBack();
        toggle();
    }

    return (
        <Fragment>
            <Button color={triggerButtonClass} onClick={toggle}>{triggerButtonLabel}</Button>
            <Modal isOpen={modal} toggle={toggle} className={className}>
                <ModalHeader toggle={toggle}>{title}</ModalHeader>
                {Children.map(props.children, child =>
                    cloneElement(child, { toggle, proceed })
                )}
            </Modal>
        </Fragment>
    )
}

export default CommomModal;