import { Result } from 'antd';
import React, { Component } from 'react';
import {
    trans
} from '../../components/CommonFunction';
import { withTranslation } from 'react-i18next';

class Error extends Component {
    render() {
        return (
            <>
                <div className="container">
                    <div style={{ marginBottom: '10px', marginTop: '100px' }}>
                        <Result
                            status={500}
                            title={this.props?.title ? this.props.title : 'Something went wrong'}
                            subTitle={this.props?.description ? this.props.description :'Sorry, something went wrong.'}
                            
                        />
                    </div>
                </div>
            </>
        );
    }

};

export default withTranslation(['common'])(Error);
