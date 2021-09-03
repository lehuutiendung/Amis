<template>
    <div class="content-wrap">
        <div class="content-area">
            <div class="content-title">
                <div class="title-row">
                    <div class="title">Nhân viên</div>
                    <Button class="green" buttonName="Thêm mới nhân viên" @click.native="showModalBox()"/>
                </div>
            </div>
            <div class="content-body"> 
                <div class="content-body-background">
                    <div class="content-body-tool">
                        <div class="content-body-tool-left" :class="{'active-button-delete' : showButtonDelete}">
                            <Button class="red" buttonName="Xóa nhiều nhân viên" @click.native="showPopupDeleteMultiple()"/>
                        </div>
                        <div class="content-body-tool-right">
                            <div class="div-input-search" :class="{'focus-input-search' : focusInput}">
                                <input class="input-search" type="text" v-model="inputSearch" 
                                placeholder="Tìm theo mã, tên nhân viên hoặc SĐT" 
                                @click.stop="inputSearchClick()" 
                                @keyup.enter="handleInputSearch($event)">
                                <div class="wrap-icon-input-search">
                                    <div class="icon-input-search"></div>
                                </div>
                            </div>
                            <div class="icon-24 btn-refresh"
                            @click="handleRefresh()"
                            @mouseover="handleShowToolContent($event, 'Lấy lại dữ liệu')" 
                            @mouseout="hideTooltipModal()"></div>
                            <div class="icon-24 btn-export" 
                            @mouseover="handleShowToolContent($event, 'Xuất ra Excel')" 
                            @mouseout="hideTooltipModal()" 
                            @click="showTableExport()"></div>
                        </div>
                    </div>
                    <div class="content-table">
                        <div class="table-wrap">
                            <table>
                                <thead>
                                    <tr>
                                        <th class="sticky-column center-column">
                                            <div class="wrap-checkbox">
                                                <input class="checkbox-column" type="checkbox" @click="handleCheckAll()">
                                                <div class="checkbox-custome" :class="{ 'checkbox-true' : checkedAll }">
                                                    <div class="border-checkbox-custome">
                                                        <div class="icon-16" :class="{ 'icon-checkbox-true' : checkedAll }">

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </th>
                                        <th class="sticky-column"><div class="th-thead">MÃ NHÂN VIÊN</div> </th>
                                        <th class="sticky-column"><div class="th-thead widen-column-230">TÊN NHÂN VIÊN</div></th>
                                        <th><div class="th-thead">GIỚI TÍNH</div></th>
                                        <th><div class="th-thead center-column">NGÀY SINH</div></th>
                                        <th><div class="th-thead widen-column-150">SỐ CMND</div></th>
                                        <th><div class="th-thead widen-column-150">CHỨC DANH</div></th>
                                        <th><div class="th-thead">TÊN ĐƠN VỊ</div></th>
                                        <th><div class="th-thead widen-column-150">SỐ TÀI KHOẢN</div></th>
                                        <th><div class="th-thead widen-column-150">TÊN NGÂN HÀNG</div></th>
                                        <th><div class="th-thead widen-column-230">CHI NHÁNH TK NGÂN HÀNG</div></th>
                                        <th><div class="th-thead widen-column-150">ĐỊA CHỈ</div></th>
                                        <th><div class="th-thead widen-column-150">ĐT DI ĐỘNG</div></th>
                                        <th><div class="th-thead widen-column-150">ĐT CỐ ĐỊNH</div></th>
                                        <th><div class="th-thead widen-column-230">EMAIL</div></th>

                                        <th class="sticky-feature"><div class="th-thead center-column">CHỨC NĂNG</div></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(employee) in employees" :key="employee.EmployeeId" @dblclick="dbClickHandle(employee.EmployeeId)">
                                        <td class="col-1-sticky">
                                            <div class="col col-1">
                                                <!-- <input class="checkbox-column" type="checkbox"> -->
                                                <div class="wrap-checkbox">
                                                    <input class="checkbox-column" type="checkbox" 
                                                    v-model="employee.checked" 
                                                    @click="handleCheckboxClick(employee, employee.EmployeeId)">
                                                    <div class="checkbox-custome" :class="{ 'checkbox-true' : employee.checked }">
                                                        <div class="border-checkbox-custome">
                                                            <div class="icon-16" :class="{ 'icon-checkbox-true' : employee.checked }"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                        <td class="col-2-sticky"><div class="col widen-column-108" :title="employee.EmployeeCode">{{ employee.EmployeeCode }}</div></td>
                                        <td class="col-3-sticky"><div class="col widen-column-230" :title="employee.FullName">{{ employee.FullName }}</div></td>
                                        <td><div class="col" :title="employee.GenderName">{{ employee.GenderName }}</div></td>
                                        <td><div class="col center-column" :title="formatDate(employee.DateOfBirth)">{{ formatDate(employee.DateOfBirth) }}</div></td>
                                        <td><div class="col" :title="employee.IdentityNumber">{{ employee.IdentityNumber }}</div></td>
                                        <td><div class="col" :title="employee.PositionName">{{ employee.PositionName }}</div></td>
                                        <td><div class="col" :title="employee.DepartmentName">{{ employee.DepartmentName }}</div></td>
                                        <td><div class="col" :title="employee.BankAccount">{{ employee.BankAccount }}</div></td>
                                        <td><div class="col" :title="employee.BankName">{{ employee.BankName }}</div></td>
                                        <td><div class="col widen-column-230" :title="employee.BankPlace">{{ employee.BankPlace }}</div></td>
                                        <td><div class="col" :title="employee.Address">{{ employee.Address }}</div></td>
                                        <td><div class="col" :title="employee.PhoneNumber">{{ employee.PhoneNumber }}</div></td>
                                        <td><div class="col" :title="employee.LandlinePhoneNumber">{{ employee.LandlinePhoneNumber }}</div></td>
                                        <td><div class="col widen-column-230" :title="employee.Email">{{ employee.Email }}</div></td>
                                        <td class="sticky-feature">
                                            <div class="feature-wrap">
                                                <div class="feature-text" @click="dbClickHandle(employee.EmployeeId)">Sửa</div>
                                                <div class="feature-icon" ref="options" @click.stop="showFeatureOption($event, employee.EmployeeId, employee.EmployeeCode, employee)"></div>
                                            </div>
                                        </td>
                                    </tr>  
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <TheFeatureOption :x="x" :y="y" 
            :showOption="showOption" 
            :employeeCode="employeeCode" 
            v-on:replicaEmployee="replicaEmployee"
            />
            <TheModalBox 
            :modalBoxShow="modalBoxShow" 
            :mode="mode" 
            :replica="replica"
            :employeeId="employeeId" 
            :employeeReplica="employeeReplica"
            v-on:exitModalBox="exitModalBox()" 
            v-on:updateTable="updateTable()" 
            v-on:updateTableAndHold="updateTableAndHold()" 
            v-on:updateEditTableAndHold="updateEditTableAndHold()"
            v-on:updateEditTable="updateEditTable()"
            v-on:showTooltipModal="showTooltipModal"
            v-on:hideTooltipModal="hideTooltipModal"
            />
            <Popup v-on:hidePopupAndForm="hidePopupAndForm()" 
            v-on:deleteEmployee="deleteEmployee()" 
            v-on:deleteMultipleEmployee="deleteMultipleEmployee()"/>
            <TheFooter :totalRecord="totalRecord" :totalPage="totalPage" 
            :maxPages="maxPages" :pages="pages" :pageNumber="pageNumber"/>
            <TooltipButton :xTooltip="xTooltip" :yTooltip="yTooltip" 
            :showTooltip="showTooltip" 
            :contentTooltip="contentTooltip"/>
            <TheTableExport :showExport="showExport" 
            :employeesTemp="employeesTemp"
            v-on:showTooltipModal="showTooltipModal"
            v-on:hideTooltipModal="hideTooltipModal" 
            v-on:exitTable="exitTable()"/>
        </div>
            <TheLoading :showLoading="showLoading"/>
            <ToastMessenger/>
    </div>
</template>
<script>
import axios from "axios";
import { EventBus } from '../../main';
import moment from "moment"
import Button from "../../components/base/BaseButton.vue"
import Popup from "../../components/base/BasePopup.vue" 
import TheFeatureOption from "../../components/base/BaseFeatureOption.vue"
import TooltipButton from "../../components/base/BaseTooltipButton.vue"
import TheLoading from "../../components/base/BaseLoading.vue"
import ToastMessenger from "../../components/base/BaseToastMessenger.vue"
import TheFooter from "../../components/layout/TheFooter.vue"
import TheModalBox from "../../components/layout/TheModalBox.vue"
import TheTableExport from "../../components/layout/TheTableExport.vue"
export default {
    name: "TheContent",
    components: {
        TheFooter,
        Button,
        TheModalBox,
        Popup,
        TheFeatureOption,
        TooltipButton,
        TheTableExport,
        TheLoading,
        ToastMessenger
    },
    data(){
        return {
            buttonName: '', //Tên trong button
            modalBoxShow: false, //Trạng thái của Form
            x: 0,   //Tọa độ click option
            y: 0,   //Tọa độ click option
            showOption: false, //Trạng thái của list item cột chức năng
            employees: [],
            employeesTemp: [], //Danh sách tạm chứa các object đã checked (checkbox đã tích) 
            employeeId: "",
            employeeCode: "",
            employeeReplica: {}, // Object lưu trữ nhân bản
            queueDelete: [], //Danh sách chứa các id của employee thực hiện xóa
            pageSize: 10,
            pageNumber: 1,
            totalPage: 0,
            totalRecord: 0,
            maxPages: 3,    //Số trang hiển thị trước dấu "..."
            startIndex: 1,
            endIndex: 10,
            pages: [],
            inputSearch: "",  //Lưu giá trị của input để truyền vào filter search
            departmentId: "", // Id của đơn vị
            focusInput: false, // Mặc định không focus vào input search
            mode: 2, // mặc định mode = 2, (thêm mới: 0, chỉnh sửa: 1)
            replica: false, //Nếu replica = true là chế độ nhân bản
            showTooltip: false, //Mặc định ẩn tooltip của các button
            xTooltip: 0, //Tọa độ của tooltip
            yTooltip: 0, //Tọa độ của tooltip
            contentTooltip: "", //Nội dung bên trong tooltip button
            checkedAll: false, //Mặc định checkbox all là false
            showButtonDelete: false, //Mặc định ẩn button "Xóa nhiều nhân viên"
            showExport: false, //Mặc định ẩn table danh sách export
            showLoading: true, //Mặc định hiện loading khi chưa load xong dữ liệu
        }
    },
    mounted() {
        //Gọi API thực hiện lấy dữ liệu phân trang
        this.callAPIFilter(this.pageSize, this.pageNumber, this.inputSearch, this.departmentId);

        // Cập nhật lại số nhân viên/trang
        EventBus.$on('updatePageSize', (value) => {
            this.pageSize = value;
        })

        // Thay đổi số nhân viên/trang => pageNumber trở về 1 (Trang đầu tiên)
        EventBus.$on('updatePageNumberV2', () => {
            this.pageNumber = 1;
        })

        // Cập nhật lại số trang hiện tại khi thực hiện Click
        EventBus.$on('updateNumberClick', (data) => {
            this.pageNumber = data;
        })

        //Trở về trước 1 trang
        EventBus.$on('prevPage', () => {
            if(this.pageNumber > 1){
                this.pageNumber--;
            }else{
                this.pageNumber = 1;
            }
        })

        //Next 1 trang
        EventBus.$on('nextPage', () => {
            if(this.pageNumber != this.totalPage){
                this.pageNumber++;
            }else{
                this.pageNumber = this.totalPage;
            }
        })

        //Ẩn list lựa chọn chức năng
        EventBus.$on('hideFeatureOption', () => {
            this.showOption = false;
        })
      
        //Thay đổi border input search
        EventBus.$on('changeBoderInput', () => {
            this.focusInput = false;
        })
    },

    methods: {
        
        /**
         * Hiển thị form thông tin
         */
        showModalBox(){
            this.modalBoxShow = !this.modalBoxShow;
            this.mode = 0;  //mode: 0 - Thêm mới
        },

        /**
         * Hiển thị button "Xóa nhiều nhân viên" khi danh sách tạm lưu trữ có object
         */
        showDelele(){
            // Hiển thị button "Xóa nhiều nhân viên" 
            if(this.employeesTemp.length > 0){
                this.showButtonDelete = true;
            }else{
                this.showButtonDelete = false;
            }
        },

        /**
         * Hiển thị tooltip trên component content
         */
        handleShowToolContent(e, content){
            this.yTooltip = e.x - 200;
            this.xTooltip = e.y + 25;
            this.contentTooltip = content;
            this.showTooltip = true;
        },    

        /**
         * HIển thị tooltip trên component modalbox
         */
        showTooltipModal(valueY, valueX, content){
            this.xTooltip = valueY;
            this.yTooltip = valueX;
            this.contentTooltip = content;
            this.showTooltip = true;
        },

        /**
         * Ẩn tooltip modalbox
         */
        hideTooltipModal(){
            this.showTooltip = false;
        },

        /**
         * Ẩn form thông tin
         */
        exitModalBox(){
            this.modalBoxShow = !this.modalBoxShow;
            this.mode = 2;
            this.replica = false;
        },
        
        /**
         * Mở form và thực hiện nhân bản
         */
        replicaEmployee(){
            this.modalBoxShow = !this.modalBoxShow;
            this.mode = 0;
            this.replica = true;
        },

        /**
         * Cập nhật lại bảng sau khi có dữ liệu thay đổi, quay về trang đầu tiên
         */ 
        updateTable(){
            this.exitModalBox();
            this.handleRefresh();
        },

        /**
         * Cập nhật bảng sau khi thêm mới, vẫn giữ hiện form
         */
        updateTableAndHold(){
            this.handleRefresh();
        },

        /**
         * Cập nhật lại bảng sau khi chỉnh sửa (Cất), giữ nguyên trang hiện tại
         */
        updateEditTable(){
            this.exitModalBox();
            this.refreshAndHoldPage();
        },

        /**
         * Cập nhật lại bảng sau khi chỉnh sửa ("Cất và thêm"), chuyển trạng thái form từ 1 -> 0 thực hiện thêm mới
         */
        updateEditTableAndHold(){
            this.handleRefresh();
            this.mode = 0;
        },

        /**
         * Nhận $emit từ popup, thực hiện ẩn form
         */
        hidePopupAndForm(){
            this.modalBoxShow = false;
            this.mode = 2;
            this.replica = false;
        },

        /**
         * Hiển thị danh sách item của cột chức năng khi được click
         */
        showFeatureOption(e, employeeId, employeeCode, employee){
            let pos = e.target.getBoundingClientRect();
            this.x = pos.top + 20;
            this.y = pos.left - 270;
            this.showOption = true;
            this.employeeId = employeeId;
            this.employeeCode = employeeCode;
            this.employeeReplica = employee;
        },

        /**
         * Hiển thị table danh sách export
         */
        showTableExport(){
            this.showExport = true;
        },
        
        /**
         * Ẩn table export
         */
        exitTable(){
            this.showExport = false;
        },

        /**
         * Xử lý phân trang
         */
        paginate(totalRecord, pageNumber, pageSize, maxPages, totalPage){
            // đảm bảo trang hiện tại không nằm ngoài phạm vi
            if (pageNumber < 1) {
                pageNumber = 1;
            } else if (pageNumber > totalPage) {
                pageNumber = totalPage;
            }

            let startPage, endPage;
            // Trường hợp tổng số trang bé hơn số trang hiển thị tối đa
            if (totalPage <= maxPages) {
                startPage = 1;
                endPage = totalPage;
            } else {
                // tổng số trang nhiều hơn tối đa => tính trang bắt đầu và trang kết thúc
                let maxPagesBeforeCurrentPage = Math.floor(maxPages / 2);
                let maxPagesAfterCurrentPage = Math.ceil(maxPages / 2) - 1;
                if (pageNumber <= maxPagesBeforeCurrentPage) {
                    // trang hiện tại gần đầu
                    startPage = 1;
                    endPage = maxPages;
                } else if (pageNumber + maxPagesAfterCurrentPage >= totalPage) {
                    // trang hiện tại gần cuối
                    startPage = totalPage - maxPages + 1;
                    endPage = totalPage;
                } else {
                    // trang hiện tại nằm ở vùng giữa
                    startPage = pageNumber - maxPagesBeforeCurrentPage;
                    endPage = pageNumber + maxPagesAfterCurrentPage;
                }
            }

            // tính chỉ số của bản ghi bắt đầu và kết thúc
            let startIndex = (pageNumber - 1) * pageSize;
            let endIndex = Math.min(startIndex + pageSize - 1, totalRecord - 1);

            // tạo một mảng các trang
            let pages = Array.from(Array((endPage + 1) - startPage).keys()).map(i => startPage + i);
            var obj = {
                totalRecord: totalRecord,
                pageNumber: pageNumber,
                pageSize: pageSize,
                totalPage: totalPage,
                startPage: startPage,
                endPage: endPage,
                startIndex: startIndex,
                endIndex: endIndex,
                pages: pages
            }
            return obj;
        },

        /**
         * Gọi API thực hiện phân trang, tìm kiếm
         */
        callAPIFilter(pageSize, pageNumber, inputValue, department){
            //Hiển thị loading chờ nhận dữ liệu
            this.showLoading = true;
            //Hiển thị loading đợi API thực hiện thành công
            axios.get(`https://localhost:44342/api/v1/Employees/Filter?pageSize=${pageSize}&pageNumber=${pageNumber}&filter=${inputValue}&departmentId=${department}`)
            .then(res => {
                this.employees = res.data.Data;
                this.totalPage = res.data.TotalPage;
                this.totalRecord = res.data.TotalRecord;
                let objPage = this.paginate(this.totalRecord, this.pageNumber, this.pageSize, this.maxPages, this.totalPage);
                this.startIndex = objPage.startIndex + 1;
                this.endIndex = objPage.endIndex + 1;
                this.pages = objPage.pages;
                //Đếm số bản ghi checked trong 1 trang, nếu đã được tích hết thì "Checkbox All" được tích
                let countChecked = 0;
                //Thêm các trường vào object của employee
                this.employees.forEach(item => {
                    //Mặc định chưa tích checkbox (Kiểm soát Css)
                    if(this.employeesTemp.length > 0){
                        if(this.saveCheckedCheckBox(item, this.employeesTemp)){
                            this.$set(item,'checked', true);
                        }
                    }
                    else this.$set(item,'checked', false);

                    //Đếm số nhân viên được chọn(checkbox)
                    if(item.checked){
                        countChecked++;
                    }
                    //CheckAll bật khi tất cả nhân viên trong trang được chọn
                    if(countChecked == this.employees.length){
                        this.checkedAll = true;
                    }else{
                        this.checkedAll = false;
                    }
                });
                
            })
            .then( ()=> {
                //Ẩn loading
                this.showLoading = false;
            })
            .catch(err => {
                console.error(err); 
                if(!err.response){
                    EventBus.$emit("showPopupWarningExport", true, 8);
                    this.showLoading = false;
                }
            })
        },
        
        /**
         * Click refresh trang
         */
        handleRefresh(){
            //Quay trở về trang đầu tiên
            this.pageNumber = 1;
            this.inputSearch = "";
            this.departmentId = "";
            //Làm mới danh sách tạm, và ẩn nút xóa nhiều
            this.employeesTemp = [];
            this.showButtonDelete = false;
            this.callAPIFilter(this.pageSize, this.pageNumber, this.inputSearch, this.departmentId);
        },

        /**
         * Refresh giữ nguyên trang hiện tại
         */
        refreshAndHoldPage(){
            this.inputSearch = "";
            this.departmentId = "";
            this.callAPIFilter(this.pageSize, this.pageNumber, this.inputSearch, this.departmentId);
        },

        /**
         * Xử lý sự kiện click input search và thay đổi border của input
         */
        inputSearchClick(){
            this.focusInput = true;
        },

        /**
         * Xử lý sự kiện nhập input và enter để tìm kiếm
         */
        handleInputSearch(e) {
            this.inputSearch = e.target.value;
            this.pageNumber = 1;
            this.callAPIFilter(this.pageSize, this.pageNumber, this.inputSearch, this.departmentId);
        },

        /**
         * Format ngày tháng hiển thị trên table
         */
        formatDate(arg){
            //Nếu có giá trị vì format
            if(arg){
                return moment(arg).format("DD/MM/YYYY");
            }else{
                //Không có giá trị ngày tháng thì để trống
                return "";
            }
        },

        /**
         * Xử lý sự kiện double click vào 1 dòng
         */
        // Double Click sửa form trong table
        dbClickHandle(employeeId) {
            this.modalBoxShow = !this.modalBoxShow;
            this.employeeId = employeeId;
            this.mode = 1;
        },

        /**
         * Thực hiện xóa 1 nhân viên
         */
        deleteEmployee(){
            this.queueDelete.push(this.employeeId);
            this.callAPIDelete(this.queueDelete);
        },

        /**
         * Hiện popup cảnh báo "xóa nhiều nhân viên"
         */
        showPopupDeleteMultiple(){
            //Danh sách chứa mã employee muốn xóa
            let queueEmployeeCode = [];
            this.employeesTemp.forEach(employee => {
                this.queueDelete.push(employee.EmployeeId);
                queueEmployeeCode.push(employee.EmployeeCode);
            });
            EventBus.$emit('showPopupDeleteMultiple', queueEmployeeCode, 5);   //typePopup = 5
            
        },

        //Thực hiện xóa nhiều nhân viên
        deleteMultipleEmployee(){
            this.callAPIDelete(this.queueDelete);
            //Sau khi xóa làm mới mảng lưu trữ tạm thời
            this.employeesTemp = [];
            this.showButtonDelete = false;
        },

        /**
         * Gọi API thực hiện xóa nhân viên
         */
        callAPIDelete(queueEmployeeId){
            axios.delete(`https://localhost:44342/api/v1/Employees`, {
                data: queueEmployeeId,      
            })
            .then(res => {
                console.log(res)
            }).then(() => {
                this.queueDelete = [];
                this.refreshAndHoldPage();
            })
            .catch(err => {
                console.error(err);
                EventBus.$emit("showPopupWarningExport", true, 8);
                this.showLoading = false; 
            })
        },

        /**
         * Xử lý sự kiện click check tất cả checkbox
         */
        handleCheckAll(){
            this.checkedAll = !this.checkedAll;
            if(this.checkedAll == true){
                this.employees.forEach(employee => {
                    //Thực hiện tìm kiếm employeeId trong danh sách tạm thời
                    let indexItem = this.employeesTemp.filter( item => item.EmployeeId == employee.EmployeeId);

                    // Nếu mảng filter được rỗng thì thêm mới đối tượng vào danh sách tạm thời
                    if (indexItem.length == 0) {
                        this.$set(employee, 'checked', true);
                        //Thêm đối tượng vào mảng lưu trữ tạm
                        this.employeesTemp.push(employee);
                    }
                });
            }else{
                this.employees.forEach(employee => {
                    this.$set(employee, 'checked', false);
                    //Tại vị trí indexItem, thực hiện remove đối tượng
                    var removeIndex = this.employeesTemp.map(function(item) { return item.EmployeeId; }).indexOf(employee.EmployeeId); 
                    this.employeesTemp.splice(removeIndex, 1);
                });
            }  
            //Hiển/Ẩn button "Xóa nhiều nhân viên"
            this.showDelele();
        },

        /**
         * Xử lý sự kiện click vào checkbox của từng dòng
         */
        handleCheckboxClick(employee, employeeId){
            //Thực hiện tìm kiếm employeeId trong danh sách tạm thời
            let indexItem = this.employeesTemp.filter( item => item.EmployeeId == employeeId);

            // Nếu mảng filter được rỗng thì thêm mới đối tượng vào danh sách tạm thời
            if (indexItem.length == 0) {
                //Thay đổi giá trị checked của đối tượng
                this.$set(employee, 'checked', true);

                //Thêm đối tượng vào danh sách tạm lưu trữ
                this.employeesTemp.push(employee);
            } else {
                //Tại vị trí indexItem, thực hiện remove đối tượng
                var removeIndex = this.employeesTemp.map(function(item) { return item.EmployeeId; }).indexOf(employeeId); 
                this.employeesTemp.splice(removeIndex, 1);

                //Thay đổi giá trị checked của đối tượng
                this.$set(employee, 'checked', false);
            }
            //Hiển/Ẩn button "Xóa nhiều nhân viên"
            this.showDelele();
        },

        /**
         * Lưu lại trạng thái checked của employee khi employee được tạo mới
         */
        saveCheckedCheckBox(employee, employeesTemp){
            for(let i = 0; i < employeesTemp.length; i++){
                if(employeesTemp[i].EmployeeId === employee.EmployeeId){
                    return true;
                }
            }
        }
    },

    watch: {
        pageNumber: function () {
            this.callAPIFilter(this.pageSize, this.pageNumber, this.inputSearch, this.departmentId);
        },

        pageSize: function (){
            this.callAPIFilter(this.pageSize, this.pageNumber, this.inputSearch, this.departmentId);
        }
    },
}
</script>
<style scoped>
    @import "../../css/layout/content.css";
</style>