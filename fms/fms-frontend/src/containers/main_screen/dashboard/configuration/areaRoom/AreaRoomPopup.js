import React, { Component } from 'react';
import { Col, Row } from "reactstrap";
import 'bootstrap/dist/css/bootstrap.min.css';
import '../../../../../layout/Common.css';
import {
    Input, Form
} from 'antd';
import 'antd/dist/antd.min.css';
import { ACTION, MESSAGE, STATUS } from '../../../../../constants/Constants';
import { AreaRoomService } from '../../../../../services/main_screen/configuration/AreaRoomService';
import '../../../../../layout/Configuration.css';
import {
    onChangeValue,
    isUndefindOrEmptyForItemForm,
    focusInvalidInput,
    validateEmpty,
    trans
} from '../../../../../components/CommonFunction';
import { Notification } from "../../../../../components/Notification";
import SelectCustom from '../../../../../components/SelectCustom';
import moment from 'moment';
import { withTranslation } from 'react-i18next';

class AreaRoomPopup extends Component {
    constructor(props) {
        super(props);
        this.state = {
            data: {},
            errors: {},
            noticeLocation: trans("configuration:areaRoom.selectCampusFirst"),
            cValue: JSON.parse(localStorage.getItem('cont')),
        };
        this.Notification = new Notification();
        this.service = new AreaRoomService();
    }

    componentDidMount() {
        let options = this.props.options;
        // console.log(options);
        if (options.action === ACTION.UPDATE) {
            this.setState({
                data: options.data,
                noticeLocation: ''
            })
        }
        this.loadLocationSelect(options.data.campusName || '');

        // options.campus.shift();
        // options.location.shift();
        this.setState({
            location: null,
            campus: options.campus,
            errors: {},
            action: options.action,
        });
        console.log(options.campus)
    }

    componentDidUpdate(prevProps) {
        let options = this.props.options;

        if (options?.data !== prevProps.options?.data) {
            // options.campus.shift();
            // options.location.shift();
            if (options.action === ACTION.UPDATE) {
                this.setState({
                    data: options.data,
                    action: options.action,
                    location: null,
                    campus: options.campus,
                    errors: {},
                    noticeLocation: ''
                });
                this.loadLocationSelect(options.data.campusName);
            } else if (options.action === ACTION.CREATE) {
                this.setState({
                    data: {},
                    errors: {},
                    location: null,
                    campus: options.campus,
                    action: options.action,
                    noticeLocation: trans("configuration:areaRoom.selectCampusFirst"),
                })

            }
        }
    }

    onCancel = () => {
        this.props.options.onCancel(this.state.data);
    }

    validate() {
        // var isValid = true;
        var data = { ...this.state.data };
        const [isValid, errors] = validateEmpty(data, ["campusName", "locationID", "name", "fullName"]);
        if (!isValid) {
            focusInvalidInput(errors);
        }

        this.setState({
            errors
        });
        return isValid;
    }

    onSubmit = () => {
        if (!this.validate()) {
            return;
        }

        let userInfo = this.context.userInfo ? this.context.userInfo : this.state.cValue.userInfo;
        let data = { ...this.state.data };

        for (let item in data) {
            if (typeof data[item] === "string") {
                data[item] = data[item].trim();
            }
        }
        if (this.state.action === ACTION.CREATE) {
            data.created = moment().format('YYYY-MM-DD');
            data.createdBy = userInfo.username;
        } else if (this.state.action === ACTION.UPDATE) {
            data.updated = moment().format('YYYY-MM-DD');
            data.updatedBy = userInfo.username;
        }
        // return;

        this.props.options.onComplete(data);
    }

    loadLocationSelect = async (value) => {
        let resListLocation = await this.service.getListLocation({
            "paging": {
                "currentPage": 1,
                "pageSize": 99999999,
                "rowsCount": 0
            },
            "campus": value,
            "locationCode": null
        });

        if (resListLocation.data.status === STATUS.SUCCESS) {
            let location = resListLocation.data.listData;
            this.setState({
                location
            });
        } else {
            this.Notification.error(MESSAGE.EROR);
        }
    }

    onChangeValueCustom = async (name, value) => {
        // debugger;
        console.log(value);
        let noticeLocation = '';
        if (!value) {
            noticeLocation = trans("configuration:areaRoom.selectCampusFirst");
        }

        let data = this.state.data;
        let errors = { ...this.state.errors };
        this.setState({
            data: {
                ...data,
                [name]: value,
                locationID: null
            },
            errors: {
                ...errors,
                campusName: '',
                locationID: ''
            },
            noticeLocation: noticeLocation
        });
        this.loadLocationSelect(value || '');

    }

    render() {
        // console.log('render')
        return (
            <>
                <Form layout="vertical">
                    <Row>
                        <Col
                            xs={12}
                            sm={12}
                            md={12}
                            lg={6}
                            xl={6}
                        >
                            <Form.Item
                                label={trans("configuration:areaRoom.campus")}
                                required={true}
                                help={this.state.errors.campusName}
                                validateStatus={isUndefindOrEmptyForItemForm(this.state.errors.campusName)}
                            >
                                <SelectCustom
                                    id="campusName"
                                    placeholder={trans("common:all")}
                                    onChange={(e, value) => this.onChangeValueCustom('campusName', e)}
                                    value={this.state.data.campusName}
                                    options={this.state.campus}
                                    keyValue={'name'}
                                />
                            </Form.Item>
                        </Col>
                        <Col
                            xs={12}
                            sm={12}
                            md={12}
                            lg={6}
                            xl={6}
                        >
                            <Form.Item
                                label={trans("configuration:areaRoom.locationCode")}
                                required={true}
                                help={this.state.errors.locationID || this.state.noticeLocation}
                                validateStatus={isUndefindOrEmptyForItemForm(this.state.errors.locationID)}
                            >
                                <SelectCustom
                                    id="locationID"
                                    placeholder={trans("common:all")}
                                    onChange={(e) => onChangeValue(this, 'locationID', e)}
                                    value={this.state.data.locationID}
                                    options={this.state.location}
                                    keyValue="id"
                                />
                            </Form.Item>
                        </Col>
                        <Col
                            xs={12}
                            sm={12}
                            md={12}
                            lg={6}
                            xl={6}
                        >
                            <Form.Item
                                label={trans("configuration:areaRoom.areaRoomCode")}
                                required={true}
                                help={this.state.errors.name}
                                validateStatus={isUndefindOrEmptyForItemForm(this.state.errors.name)}
                            >
                                <Input
                                    id="name"
                                    placeholder=''
                                    value={this.state.data?.name}
                                    onChange={e => onChangeValue(this, 'name', e.target.value)}
                                >
                                </Input>
                            </Form.Item>
                        </Col>
                        <Col
                            xs={12}
                            sm={12}
                            md={12}
                            lg={6}
                            xl={6}
                        >
                            <Form.Item
                                label={trans("configuration:areaRoom.areaFullName")}
                                required={true}
                                help={this.state.errors?.fullName}
                                validateStatus={isUndefindOrEmptyForItemForm(this.state.errors.fullName)}
                            >
                                <Input
                                    id="fullName"
                                    placeholder=''
                                    value={this.state.data?.fullName}
                                    onChange={e => onChangeValue(this, 'fullName', e.target.value)}
                                >
                                </Input>
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row style={{ display: 'flex', justifyContent: 'flex-end' }}
                        noGutters
                        className="modal-footer-custom"
                    >
                        <Col
                            xs={6}
                            sm={3}
                            md={2}
                            lg={2}
                            xl={1}
                            style={{ display: 'flex', justifyContent: 'flex-end' }}
                        >
                            <div className="button button-submit" onClick={this.onSubmit}>
                            {trans("common:button.submit")}
                            </div>
                        </Col>
                        <Col
                            xs={6}
                            sm={3}
                            md={2}
                            lg={2}
                            xl={1}
                            style={{ display: 'flex', justifyContent: 'flex-end', marginLeft: '10px' }}
                        >
                            <div className="button" onClick={this.onCancel}>
                            {trans("common:button.cancel")}
                            </div>
                        </Col>
                    </Row>
                </Form>
            </>
        );
    }

};

export default withTranslation(['configuration', 'common'])(AreaRoomPopup);
