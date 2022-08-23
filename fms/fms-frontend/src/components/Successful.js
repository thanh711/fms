import { Result, Button } from 'antd';
import React, { Component } from 'react';
import { Link } from 'react-router-dom';

class Successful extends Component {

    onCreateAnother = () => {
        this.props.options.onCreateAnother();
    }

    render() {
        return (
            <>
                <div className="container">
                    <div style={{ marginTop: '100px' }}>
                        <Result
                            status={'success'}
                            title={'Create trouble report successfully!'}
                            subTitle={'Thanks for the information you provided, we will get back to you soon!'}
                            extra={
                                [
                                    <Link
                                        to={{ pathname: '/detailTrouble/' + this.props.options.reportID }}
                                    >
                                        <Button className='button-submit button' style={{ height: '40px' }}>
                                            View Detail
                                        </Button>
                                    </Link>,

                                    <Link
                                        to={{ pathname: '/createTrouble'}}
                                    >
                                        <Button className='button-other button' style={{ height: '40px' }} onClick={this.onCreateAnother}>
                                            Create Another
                                        </Button>
                                    </Link>

                                ]
                            }
                        />
                    </div>
                </div>
            </>
        );
    }

};

export default Successful;
