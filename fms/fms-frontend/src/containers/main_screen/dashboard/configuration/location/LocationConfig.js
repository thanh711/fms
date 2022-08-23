import React, { Component } from 'react';
import { Col, Row } from "reactstrap";
import 'bootstrap/dist/css/bootstrap.min.css';
import '../../../../../layout/Common.css';
import {
    Button, Checkbox, Table, Pagination, Select, Input
} from 'antd';
import 'antd/dist/antd.min.css';
import HeaderPannel from '../../../../../components/HeaderPannel';
import { STATUS, MESSAGE } from '../../../../../constants/Constants';
import { LocationService } from '../../../../../services/main_screen/configuration/LocationService';
import { CampusService } from '../../../../../services/main_screen/configuration/CampusService';
import '../../../../../layout/Configuration.css';
import LocationPopup from './LocationPopup';
import { Notification } from '../../../../../components/Notification';
import removeIcon from '../../../../../assets/bin.png';
import editIcon from '../../../../../assets/edit.png';
import { hideDialogConfirm, showConfirm } from '../../../../../components/MessageBox';
import {
    handleHideNav,
    trans
} from '../../../../../components/CommonFunction';
import { showDialog, hideDialog } from '../../../../../components/Dialog';
import LocationImport from './LocationImport';
import { withTranslation } from 'react-i18next';
import { SettingOutlined } from '@ant-design/icons';

class LocationConfiguration extends Component {
    constructor(props) {
        super(props);
        this.state = {
            data: [],
            filter: {
                paging: {
                    pageSize: 10,
                    currentPage: 1,
                    rowsCount: 0
                },
                locationCode: null,
                campus: null
            },
            dialog: {
                show: false,
                isEdit: false,
                id: 0
            },
            campus: [],
            columns: this.columns
        }
        this.LocationService = new LocationService();
        this.Notification = new Notification();
        this.CampusService = new CampusService();
    }

    componentDidMount() {
        this.loadForm();
        this.getCampusData();
        document.querySelector('.container').addEventListener('click', handleHideNav);
    }

    loadForm = async (currentPage = 1) => {
        let filter = { ...this.state.filter };
        filter.paging.currentPage = currentPage;
        if (typeof filter.locationCode === 'string') {
            filter.locationCode = filter.locationCode.trim();
            if (filter.locationCode.length === 0) {
                filter.locationCode = null;
            }
        }
        this.LocationService.getListNoCondition(filter).then(res => {
            if (res && res.status === 200) {
                if (res.data) {
                    let filter = { ...this.state.filter };
                    filter.paging.rowsCount = res.data.paging.rowsCount;
                    let data = res.data.listData;
                    let stt = 1;
                    for (let item of data) {
                        item.stt = stt;
                        stt++;
                    }
                    this.setState({ data: data, filter: filter });
                }
            } else {
                if (res) {
                    this.Notification.error(res.message);
                }
            }
        });
    }

    getCampusData = async () => {
        await this.CampusService.getAllNoCondition().then(res => {
            if (res && res.status === 200) {
                if (res.data && res.data.listData) {
                    this.setState({ campus: res.data.listData });
                }
            }

        })
    };

    _onCloseDialog = (callBack = false) => {
        let dialog = { ...this.state.LocationPopup };
        dialog.show = false;
        dialog.isEdit = false;
        dialog.row = {};
        this.setState({ LocationDialog: dialog });
        if (callBack) {
            setTimeout(() => {
                this.loadForm();
            }, 100);
        }
    };

    _onOpenDialog = (record, isEdit = false) => {
        let dialog = { ...this.state.LocationDialog };
        dialog.show = true;
        dialog.isEdit = isEdit;
        dialog.row = record;
        this.setState({ LocationDialog: dialog });
    };

    _openDeletePopup = (id) => {
        showConfirm(
            "Are you sure to delete?",
            () => this.onDelete(id),
            "Notification"
        )
    }

    onDelete = (id) => {
        this.LocationService.delete(id).then(res => {
            if (res && res.status === 200) {
                this.Notification.success("Delete successfully!");
            }
        });
        setTimeout(() => {
            hideDialogConfirm();
            this.loadForm();
        }, 100);
    };

    onShowSizeChange = (current, pageSize) => {
        let filter = { ...this.state.filter };
        filter.paging.currentPage = 1;
        filter.paging.pageSize = pageSize;
        this.setState({ filter: filter });

        setTimeout(() => {
            this.loadForm();
        }, 100);

    };


    onPageChange = (page, pageSize) => {
        let filter = { ...this.state.filter };
        filter.paging.currentPage = page;
        this.setState({ filter: filter });
        setTimeout(() => {
            this.loadForm(page);
        }, 100);

    };

    onChange = value => {
        let data = { ...this.state.filter };
        data.campus = value;
        this.setState({ filter: data });
    }

    onCheckIsActive = async (value, dataRow) => {
        let resIsActive = await this.LocationService.changeIsActive({
            id: dataRow.id,
            inService: value
        });
        if (resIsActive && resIsActive.data.status === STATUS.SUCCESS) {
            let filter = { ...this.state.filter };
            this.Notification.success("Change in service successfully.")
            this.loadForm(filter.paging.currentPage);
        } else {
            this.Notification.error(MESSAGE.ERROR);
        }
    }

    renderColumnCheckbox(dataCell, dataRow) {
        return <div style={{ textAlign: 'center' }}>
            <Checkbox checked={dataCell} onChange={e => this.onCheckIsActive(e.target.checked, dataRow)}></Checkbox>
        </div >
    }

    onOpenDialogImport = () => {
        showDialog(
            this.getImportForm(),
            trans("configuration:importFromExcel"),
        )
    }

    getImportForm = () => {
        return (
            <LocationImport
                options={{
                    onComplete: async (rowData) => {
                        const res = await this.service.saveImport(rowData);
                        console.log(res.data.data, 'check res.data');

                        if (res.data.status === STATUS.SUCCESS) {
                            hideDialog();
                            let update = res.data?.data;

                            let data = { ...this.state.data };
                            console.log(data, 'check data');
                            data.componentId = update.componentId;
                            await this.setState({
                                data: data
                            });
                            this.loadForm();
                            this.Notification.success(MESSAGE.UPDATE_SUCCESS);


                        } else {
                            this.Notification.error(MESSAGE.ERROR);
                        }
                    },
                    onCancel: (rowData) => {
                        // console.log('ádasdas')
                        hideDialog(false, rowData);
                    }
                }}
            />
        )
    }

 
    render() {
        console.log(this.state.data);
   var columns = [
        {
            title: trans('configuration:no'),
            dataIndex: 'stt',
            key: 'stt',
            width: '5%'
        },
        {
            title: trans('configuration:location.campus'),
            dataIndex: 'campusName',
            key: 'campusName',
            width: 80,
        },
        {
            title:  trans('configuration:location.locationCode'),
            dataIndex: 'code',
            key: 'code',
            width: 130,
        },
        {
            title:  trans('configuration:location.locationName'),
            dataIndex: 'name',
            key: 'name',
            width: 150,
        },
        {
            title:  trans('configuration:location.locationFullName'),
            dataIndex: 'fullName',
            key: 'fullName',
            width: 180,
        },
        {
            title: trans('configuration:inService'),
            dataIndex: 'inService',
            key: 'inService',
            width: 80,
            render: (dataCell, dataRow) => this.renderColumnCheckbox(dataCell, dataRow)
        },
        {
            title: <SettingOutlined/>,
            dataIndex: 'action',
            key: 'action',
            width: 60,
            render: (review, record) => <div style={{ textAlign: 'center', justifyContent: 'space-around', display: 'flex' }}>
                <Button
                    type='link'
                    title='Edit'
                    onClick={() => {
                        this._onOpenDialog(record, true);
                    }}
                >
                    <img src={editIcon} alt="" width="20px" />
                </Button>
            </div>
        }
    ];

        return (
            <>
                <div className="container">
                    <Row>
                        <Col
                            style={{ marginBottom: "10px" }}
                            xs={12}
                            sm={12}
                            md={12}
                            lg={12}
                            xl={12}
                        >
                            <HeaderPannel
                                title={trans('configuration:location.title')}
                                breadcrumbList={[trans('configuration:location.configuration'), trans('configuration:location.location')]}
                                buttons={[
                                    {
                                        title: trans("common:button.import"),
                                        classNameCustom: 'submit',
                                        action: () => this.onOpenDialogImport
                                    },
                                    {
                                        title: trans("common:button.create"),
                                        classNameCustom: 'submit',
                                        action: () => this._onOpenDialog
                                    }
                                ]}
                            />
                        </Col>
                        <Col xs={12} sm={12} md={12} lg={12} xl={12} style={{ marginBottom: "10px" }}>
                            <div className="daily-checklist-container border-form padding-pannel">
                                <Row >
                                    <Col xs={12} sm={12} md={5} lg={4} xl={3}>
                                        <div className="location-search-title">
                                        {trans('configuration:location.campus')}
                                        </div>
                                        <div className="location-search-combobox">
                                            <Select
                                                className="dropdown-fix-width"
                                                showSearch
                                                mode={"combobox"}
                                                onChange={this.onChange}
                                                placeholder={trans("common:all")}
                                                allowClear={true}
                                            >
                                                {
                                                    this.state.campus.map(item => {
                                                        return <Select.Option value={item.name}> {item.name}</Select.Option>
                                                    })
                                                }
                                            </Select>
                                        </div>
                                    </Col>
                                    <Col xs={12} sm={12} md={5} lg={4} xl={3}>
                                        <div className="location-search-title">
                                        {trans('configuration:location.locationCode')}
                                        </div>
                                        <div className="location-search-combobox">
                                            <Input
                                                name='locationCode'
                                                value={this.state.filter.locationCode}
                                                onChange={(e) => {
                                                    let filter = { ...this.state.filter };
                                                    filter.locationCode = e.target.value.length === 0 ? null : e.target.value;
                                                    this.setState({ filter: filter });
                                                }}
                                                placeholder=""
                                            />
                                        </div>
                                    </Col>
                                    <Col xs={12} sm={12} md={2} lg={4} xl={6} style={{
                                        marginTop: '24px',
                                        display: 'flex',
                                        justifyContent: 'flex-end'
                                    }}>
                                        <Button
                                            className="button-submit button"
                                            onClick={() => { this.loadForm() }}
                                        >
                                           {trans("common:button.search")}
                                        </Button>
                                    </Col>
                                    <Col xs={12} sm={12} md={12} lg={12} xl={12} style={{ marginTop: "15px" }}>
                                        <Table
                                            columns={columns}
                                            dataSource={this.state.data}
                                            size="small"
                                            pagination={false}
                                            scroll={{
                                                y: 240,
                                            }}
                                        />

                                        <div className="page-div">
                                            <Pagination
                                                showSizeChanger
                                                onShowSizeChange={this.onShowSizeChange}
                                                showTotal={(total, range) => { return (<span>{range[0]} - {range[1]} {trans('common:of')} {total} {trans('common:items')}</span>) }}
                                                onChange={this.onPageChange}
                                                defaultCurrent={1}
                                                total={this.state.filter.paging.rowsCount}
                                                current={this.state.filter.paging.currentPage}
                                                pageSizeOptions={['1', '10', '20', '30', '40']}
                                                defaultPageSize={15}
                                                pageSize={this.state.filter.paging.pageSize}
                                            />
                                        </div>
                                    </Col>
                                </Row>
                            </div>
                        </Col>
                    </Row>
                </div>

                <LocationPopup
                    {...this.state.data}
                    onCloseDialog={this._onCloseDialog}
                    {...this.state.LocationDialog}
                />
            </>
        );
    }

};

export default withTranslation(['configuration', 'common'])(LocationConfiguration);
