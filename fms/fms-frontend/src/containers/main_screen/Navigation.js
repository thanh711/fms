import React, { Component } from 'react';
import 'antd/dist/antd.min.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import {
    MenuOutlined, LogoutOutlined, HomeOutlined, UnorderedListOutlined,
    FormOutlined, SettingOutlined, SolutionOutlined, UserOutlined
} from '@ant-design/icons';
import '../../layout/Navigation.css';
import { Link, Navigate, Outlet } from 'react-router-dom';
import '../../layout/Common.css';
import { Button, Image, Select } from 'antd';
import AppContext from '../../context/AppContext';
import logoutIcon from '../../assets/logout.png';
import logoutIcon2 from '../../assets/logoutIcon.png';
import warehouseIcon from '../../assets/warehouse2.png';
import logoFms from '../../assets/LogoDashboard.png';
import vietnam from '../../assets/vietnam.png';
import usaFlag from '../../assets/usaFlag.png';
import troubleIcon from '../../assets/trouble.png'
import mapIcon from '../../assets/map.png';
import { withTranslation, Trans } from 'react-i18next';
import {
    trans
} from '../../components/CommonFunction';

class Dashboard extends Component {
    static contextType = AppContext;

    constructor(props) {
        super(props);
        this.state = {
            welcomeName: '',
            backgroundChoose: 'background-item-choose',
            itemChooose: '',
            menuItems: [
                {
                    name: 'Home',
                    icon: <HomeOutlined />,
                    link: 'home'
                },
                {
                    name: 'Trouble',
                    icon: <Image
                        src={troubleIcon}
                        preview={false}
                        width={20}
                    ></Image>,
                    link: 'trouble'
                },
                {
                    name: 'Checklist',
                    icon: <UnorderedListOutlined />,
                    link: 'checklist'
                },
                // {
                //     name: 'Maintaince',
                //     icon: <ToolOutlined />
                // },
                {
                    name: 'Techreport',
                    icon: <FormOutlined />,
                    link: 'techreport'
                },
                {
                    name: 'Map',
                    icon: <Image
                        src={mapIcon}
                        preview={false}
                        width={20}
                    ></Image>,
                    link: 'mapView'
                },
                {
                    name: 'Warehouse',
                    icon: <Image
                        src={warehouseIcon}
                        preview={false}
                        width={20}
                    ></Image>,
                    // link: 'warehouseAsset'
                },
                {
                    name: 'Configuration',
                    icon: <SettingOutlined />,
                    link: 'campusConfiguration'
                },
                {
                    name: 'Guideline',
                    icon: <SolutionOutlined />,
                    link: 'guideline'
                },
            ],
            cValue: JSON.parse(localStorage.getItem('cont'))
        }
    }

    componentDidMount() {
    }

    onChangeMenuItem = (e, name) => {
        let userInfo = JSON.parse(localStorage.getItem('cont'));
        var url = window.location.href;

        console.log(name)
        if (!userInfo) {
            if (url.slice(url.lastIndexOf('/') + 1) === 'createTrouble') {
                const login = document.createElement('a');
                login.href = '/createTroubleWithoutLogin';
                login.click();
                login.remove();
            } else {
                console.log('logout')
                const login = document.createElement('a');
                login.href = '/login';
                login.click();
                login.remove();
            }
        }
        let backgroundChoose = 'background-item-choose';
        var itemChooose = url.slice(url.lastIndexOf('/') + 1);
        console.log(itemChooose, 'check url');
        let elementChose;
        let menuItems = this.state.menuItems;
        for (let i = 0; i < menuItems.length; i++) {
            let menuItemsName = menuItems[i].name.toLowerCase();
            if (menuItemsName !== itemChooose) {
                elementChose = document.querySelector('.' + menuItemsName + '-navigator');
                elementChose?.classList.remove(backgroundChoose);
            }
        }

        elementChose = document.querySelector('.' + name + '-navigator');
        elementChose.classList.add(this.state.backgroundChoose);

    }

    handleShowNav() {
        let target = document.getElementById('navigation-bar');
        console.log(target);
        let hasClass = target.classList.contains('show-navigation');
        if (hasClass) {
            target.classList.remove('show-navigation');
        } else {
            target.classList.add('show-navigation');
        }
    }

    handleHideNav() {
        let target = document.getElementById('navigation-bar');
        console.log(target);
        let hasClass = target.classList.contains('show-navigation');
        if (hasClass) {
            target.classList.remove('show-navigation');
        }
    }



    render() {
        let userInfo = this.state.cValue?.userInfo;
        var url = window.location.href;
        var itemChooose = url.slice(url.indexOf('/', 7) + 1);
        var backgroundChose = 'background-item-choose';

        let menuElement = this.state.menuItems.map((item, index) => {
            let classBackgroundChose = item.name.toLowerCase() === itemChooose ? backgroundChose : '';
            if (itemChooose.toLowerCase().indexOf(item.name.toLowerCase()) !== -1) {
                classBackgroundChose = backgroundChose;
            } else if (itemChooose === '' && item.name.toLowerCase() === 'home') {
                classBackgroundChose = backgroundChose;
            }
            let displayNone = null;
            let renderNav = null;
            if (userInfo?.role === 1 && (item.name.toLowerCase() === 'home' || item.name.toLowerCase() === 'trouble' || item.name.toLowerCase() === 'map')) {
                renderNav = <li className={item.name.toLowerCase() + '-navigator ' + classBackgroundChose} key={index}
                    onClick={(e) => this.onChangeMenuItem(e, item.name.toLowerCase())}>
                    {
                        item.link ?
                            <Link to={item.link} >
                                <div className='icon-navigator'>
                                    {item.icon}
                                </div>
                                <div className='title-navigator'>{item.name}</div>
                            </Link> :
                            <>
                                <div className='icon-navigator'>
                                    {item.icon}
                                </div>
                                <div className='title-navigator'>{item.name}</div>
                            </>
                    }

                    {item.name === 'Trouble' ?
                        <ul className='subNab-troubles subNab'>
                            <li><Link to='trouble'>My Troubles</Link></li>
                            <li><Link to='createTrouble'>Create Trouble</Link></li>
                        </ul> : ''
                    }
                </li>
            } else if (userInfo?.role === 2) {
                renderNav = <li className={item.name.toLowerCase() + '-navigator ' + classBackgroundChose} key={index}
                    onClick={(e) => this.onChangeMenuItem(e, item.name.toLowerCase())} style={displayNone}>
                    {
                        item.link ?
                            <Link to={item.link} >
                                <div className='icon-navigator'>
                                    {item.icon}
                                </div>
                                <div className='title-navigator'>{item.name}</div>
                            </Link> :
                            <>
                                <div className='icon-navigator'>
                                    {item.icon}
                                </div>
                                <div className='title-navigator'>{item.name}</div>
                            </>
                    }

                    {item.name === 'Trouble' ?
                        <ul className='subNab-troubles subNab'>
                            <li><Link to='trouble'>{trans('navigation:myTrouble')}</Link></li>
                            <li><Link to='createTrouble'>{trans('navigation:createTrouble')}</Link></li>
                        </ul> : ''
                    }
                    {item.name === 'Checklist' ?
                        <ul className='subNab-checklists subNab'>
                            <li><Link to='checklist'>{trans('navigation:myChecklist')}</Link></li>
                            <li><Link to='checklistCustomize'>{trans('navigation:customizeChecklist')}</Link></li>
                        </ul> : ''
                    }
                    {item.name === 'Configuration' ?
                        <ul className='subNab-configuration subNab'>
                            <li><Link to='campusConfiguration'>{trans('navigation:campus')}</Link></li>
                            <li><Link to='locationConfiguration'>{trans('navigation:location')}</Link></li>
                            <li><Link to='roomAreaConfiguration'>{trans('navigation:areaRoom')}</Link></li>
                            <li><Link to='assetConfiguration'>{trans('navigation:asset')}</Link></li>
                            <li><Link to='userConfiguration'>{trans('navigation:user')}</Link></li>
                            <li><Link to='assetCategoryConfiguration'>{trans('navigation:assetType')}</Link></li>
                        </ul> : ''
                    }
                    {item.name === 'Warehouse' ?
                        <ul className='subNab-warehouse subNab'>
                            <li><Link to='warehouseHistory'>{trans('navigation:history')}</Link></li>
                            <li><Link to='warehouseRemainingReport'>{trans('navigation:remainingReport')}</Link></li>
                            <li><Link to='warehouseRemainingStandard'>{trans('navigation:remainingStandards')}</Link></li>
                        </ul> : ''
                    }
                    {item.name === 'Map' ?
                        <ul className='subNab-map subNab'>
                            <li><Link to='mapView'>{trans('navigation:viewMap')}</Link></li>
                            <li><Link to='mapCustomize'>{trans('navigation:customizeMap')}</Link></li>
                        </ul> : ''
                    }
                </li>
            } else if (userInfo?.role === 3 && item.name.toLowerCase() !== 'configuration') {
                renderNav = <li className={item.name.toLowerCase() + '-navigator ' + classBackgroundChose} key={index}
                    onClick={(e) => this.onChangeMenuItem(e, item.name.toLowerCase())} style={displayNone}>
                    {
                        item.link ?
                            <Link to={item.link} >
                                <div className='icon-navigator'>
                                    {item.icon}
                                </div>
                                <div className='title-navigator'>{item.name}</div>
                            </Link> :
                            <>
                                <div className='icon-navigator'>
                                    {item.icon}
                                </div>
                                <div className='title-navigator'>{item.name}</div>
                            </>
                    }
                    {item.name === 'Trouble' ?
                        <ul className='subNab-troubles subNab'>
                            <li><Link to='trouble'>{trans('navigation:myTrouble')}</Link></li>
                            <li><Link to='createTrouble'>{trans('navigation:createTrouble')}</Link></li>
                        </ul> : ''
                    }
                    {item.name === 'Checklist' ?
                        <ul className='subNab-checklists subNab'>
                            <li><Link to='checklist'>{trans('navigation:myChecklist')}</Link></li>
                            <li><Link to='checklistCustomize'>{trans('navigation:customizeChecklist')}</Link></li>
                        </ul> : ''
                    }
                    {item.name === 'Warehouse' ?
                        <ul className='subNab-warehouse subNab'>
                            <li><Link to='warehouseAsset'>{trans('navigation:assetImportExport')}</Link></li>
                            <li><Link to='warehouseHistory'>{trans('navigation:history')}</Link></li>
                            <li><Link to='warehouseRemainingReport'>{trans('navigation:remainingReport')}</Link></li>
                            <li><Link to='warehouseRemainingStandard'>{trans('navigation:remainingStandards')}</Link></li>
                        </ul> : ''
                    }
                    {item.name === 'Map' ?
                        <ul className='subNab-map subNab'>
                            <li><Link to='mapView'>{trans('navigation:viewMap')}</Link></li>
                            <li><Link to='mapCustomize'>{trans('navigation:customizeMap')}</Link></li>
                        </ul> : ''
                    }
                </li>
            } else if (userInfo?.role === 4 && item.name.toLowerCase() !== 'configuration' && item.name.toLowerCase() !== 'techreport') {
                renderNav = <li className={item.name.toLowerCase() + '-navigator ' + classBackgroundChose} key={index}
                    onClick={(e) => this.onChangeMenuItem(e, item.name.toLowerCase())} style={displayNone}>
                    {
                        item.link ?
                            <Link to={item.link} >
                                <div className='icon-navigator'>
                                    {item.icon}
                                </div>
                                <div className='title-navigator'>{item.name}</div>
                            </Link> :
                            <>
                                <div className='icon-navigator'>
                                    {item.icon}
                                </div>
                                <div className='title-navigator'>{item.name}</div>
                            </>
                    }

                    {item.name === 'Trouble' ?
                        <ul className='subNab-troubles subNab'>
                            <li><Link to='trouble'>{trans('navigation:myTrouble')}</Link></li>
                            <li><Link to='createTrouble'>{trans('navigation:createTrouble')}</Link></li>
                        </ul> : ''
                    }
                    {item.name === 'Warehouse' ?
                        <ul className='subNab-warehouse subNab' style={{ top: '260px' }}>
                            <li><Link to='warehouseHistory'>{trans('navigation:history')}</Link></li>
                            <li><Link to='warehouseRemainingReport'>{trans('navigation:remainingReport')}</Link></li>
                        </ul> : ''
                    }
                </li>
            }
            return (
                renderNav
            );
        });
        const { t, i18n } = this.props;
        var urlString = url.slice(url.lastIndexOf('/') + 1);
        console.log(urlString, 'check urlString')
        return (
            <>
                {
                    !userInfo ?
                        <>
                            {
                                urlString === 'createTrouble' ?
                                    <Navigate to="/createTroubleWithoutLogin" replace={true}></Navigate>
                                    :
                                    <Navigate to="/login" replace={true}></Navigate>
                            }
                        </>
                        :
                        <>
                            <div>
                                <div className='header'>
                                    <div className='right-header'>
                                        <div className='menu-reponsive-icon hide-menu-icon' onClick={this.handleShowNav}>
                                            <MenuOutlined style={{ color: 'white', color: 'white', top: '25px', position: 'relative' }} />
                                        </div>
                                        <div className='logo-fms'><Image src={logoFms} preview={false} /></div>
                                        <div className='title-header'>{trans('navigation:titleHeader')}</div>
                                    </div>
                                    <div className='left-header'>
                                        <div className='welcome-title'>
                                            <span className='welcome-word'>{trans('navigation:welcome')}, </span>
                                            <span className='welcome-name'>{userInfo?.name}</span>
                                        </div>
                                        <div className='fptuni-container'>
                                            {/* <div className='fptuni-logo'>
                                                <div className='fptuni-image'></div>
                                                <div className='fptuni-title'>Fpt University</div>
                                            </div> */}
                                            {/* <span style={{color: 'white'}}>Lang</span> */}
                                            <Select className="select-lan" defaultValue={"vn"} onChange={(e) => i18n.changeLanguage(e)} showArrow={false}>
                                                <Select.Option value="vn">
                                                    <Image src={vietnam} preview={false} width={20} />
                                                </Select.Option>
                                                <Select.Option value="en">
                                                    <Image src={usaFlag} preview={false} width={20} />
                                                </Select.Option>
                                            </Select>
                                        </div>
                                        {/* <div className='fptuni-container'>
                                            <div className='fptuni-logo'>
                                                <div className='fptuni-image'></div>
                                                <div className='fptuni-title'>Fpt University</div>
                                            </div>
                                        </div> */}
                                        <div >
                                            <Button type='link'
                                                style={{ display: 'flex', justifyContent: 'center', flexDirection: 'column' }}
                                                onClick={e => {
                                                    localStorage.clear();
                                                    this.setState({ cValue: null });
                                                }}
                                            >
                                                <Image src={logoutIcon} preview={false} width={30} />
                                            </Button>
                                        </div>
                                    </div>
                                    <div className="menu-reponsive-container">
                                        <div style={{ marginLeft: '10px' }}>
                                            <Select className="select-lan" defaultValue={"vn"} onChange={(e) => i18n.changeLanguage(e)} showArrow={false}>
                                                <Select.Option value="vn">
                                                    <Image src={vietnam} preview={false} width={20} />
                                                </Select.Option>
                                                <Select.Option value="en">
                                                    <Image src={usaFlag} preview={false} width={20} />
                                                </Select.Option>
                                            </Select>
                                        </div>
                                        <div className='menu-reponsive-icon' onClick={this.handleHideNav}>
                                            <UserOutlined style={{ color: 'white' }} />
                                            <ul className='subNab-responsive'>
                                                <li>{trans('navigation:welcome')}, {userInfo?.username}</li>
                                                <li>
                                                    <Button type='link'
                                                        style={{ display: 'flex', padding: '5px 0px' }}
                                                        onClick={e => {
                                                            localStorage.clear();
                                                            this.setState({ cValue: null });
                                                        }}
                                                    >
                                                        {/* <LogoutOutlined style={{ color: 'black', fontSize: '5px' }} /> */}
                                                        <Image src={logoutIcon2} preview={false} width={18} />
                                                        <span style={{marginLeft: '5px'}}>
                                                            {trans('navigation:signOut')}
                                                        </span>
                                                    </Button>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div id='navigation-bar' className='navigation-bar'>
                                    <ul className='list-navigation'>
                                        {menuElement}
                                    </ul>
                                </div>
                                <div className='footer' onClick={this.handleHideNav}>
                                    <div className='guideline-direction'>
                                        <div className='guideline-icon'>
                                            <SolutionOutlined />
                                        </div>
                                        <div className='guideline-link' onClick={(e) => this.onChangeMenuItem(e, 'guideline')}>
                                            <Link to='guideline'>
                                                {trans('navigation:guideline')}
                                            </Link>
                                        </div>
                                    </div>
                                    <div className='infor-project'>
                                        <span><b>FPT University</b> © <b>2022</b>, SWP490_G7</span>
                                    </div>
                                </div>
                                <Outlet />
                            </div>
                        </>
                }
            </>
        );
    }

};

export default withTranslation('navigation')(Dashboard);
