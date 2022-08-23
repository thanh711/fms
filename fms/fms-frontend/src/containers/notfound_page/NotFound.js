import { Result } from 'antd';
import React, { Component } from 'react';
import {
    trans
} from '../../components/CommonFunction';
import { withTranslation } from 'react-i18next';

class Notfound extends Component {
    render() {
        return (
            <>
                <div className="container">
                    <div style={{ marginBottom: '10px' }}>
                        <Result
                            status={404}
                            title={'Not Found'}
                            subTitle={this.props?.description ? this.props.description : 'Sorry, the page you visited does not exist.'}
                        />
                    </div>
                </div>
            </>
        );
    }

};

export default withTranslation(['common'])(Notfound);
