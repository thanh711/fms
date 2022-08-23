import { Result, Button } from 'antd';
import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import {
    trans
} from '../../components/CommonFunction';
import { withTranslation } from 'react-i18next';

class Unauthozied extends Component {
    render() {
        return (
            <>
                <div className="container">
                    <div style={{ marginBottom: '10px' }}>
                        <Result
                            status="403"
                            title="403"
                            subTitle="Sorry, you are not authorized to access this page."
                            extra={<Button style={{ backgroundColor: '#FFB31E', color: '#fff' }}>
                                <Link to="/home" replace>
                                    Back Home
                                </Link>
                            </Button>}
                        />
                    </div>
                </div>
            </>
        );
    }

};

export default withTranslation(['common'])(Unauthozied);
