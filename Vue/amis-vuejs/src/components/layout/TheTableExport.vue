<template>
    <div class="table-export-background" :class="{'active-table-export' : showExport}">
        <div class="wrap-table-export">
            <div class="content-body"> 
                <div class="content-body-background">
                    <div class="title-table-wrap">
                        <div class="title-table">Danh sách xuất khẩu nhân viên</div>    
                        <div class="exit-table">
                            <div class="icon-24 icon-exit-table" 
                            @mouseover="handleShowTooltip($event, 'Đóng (ESC)')"
                            @mouseout="handleHideTooltip()"
                            @click="exitTable()">
                            </div>
                        </div>
                    </div>
                    <div class="content-table">
                        <div class="table-wrap">
                            <table>
                                <thead>
                                    <tr>
                                        <th class="sticky-column"><div class="th-thead">MÃ NHÂN VIÊN</div> </th>
                                        <th class="sticky-column"><div class="th-thead widen-column-230">TÊN NHÂN VIÊN</div></th>
                                        <th><div class="th-thead">GIỚI TÍNH</div></th>
                                        <th><div class="th-thead center-column">NGÀY SINH</div></th>
                                        <th><div class="th-thead widen-column-150">SỐ CMND</div></th>
                                        <th><div class="th-thead widen-column-150">CHỨC DANH</div></th>
                                        <th><div class="th-thead">TÊN ĐƠN VỊ</div></th>
                                        <th><div class="th-thead widen-column-150">SỐ TÀI KHOẢN</div></th>
                                        <th><div class="th-thead widen-column-230">TÊN NGÂN HÀNG</div></th>
                                        <th><div class="th-thead widen-column-230">CHI NHÁNH TK NGÂN HÀNG</div></th>
                                        <th><div class="th-thead widen-column-150">ĐỊA CHỈ</div></th>
                                        <th><div class="th-thead widen-column-150">ĐT DI ĐỘNG</div></th>
                                        <th><div class="th-thead widen-column-150">ĐT CỐ ĐỊNH</div></th>
                                        <th><div class="th-thead widen-column-230">EMAIL</div></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(employee) in employeesTemp" :key="employee.EmployeeId">
                                        <td class="col-2-sticky"><div class="col" :title="employee.EmployeeCode">{{ employee.EmployeeCode }}</div></td>
                                        <td class="col-3-sticky"><div class="col" :title="employee.FullName">{{ employee.FullName }}</div></td>
                                        <td><div class="col" :title="employee.GenderName">{{ employee.GenderName }}</div></td>
                                        <td><div class="col" :title="formatDate(employee.DateOfBirth)">{{ formatDate(employee.DateOfBirth) }}</div></td>
                                        <td><div class="col" :title="employee.IdentityNumber">{{ employee.IdentityNumber }}</div></td>
                                        <td><div class="col" :title="employee.PositionName">{{ employee.PositionName }}</div></td>
                                        <td><div class="col" :title="employee.DepartmentName">{{ employee.DepartmentName }}</div></td>
                                        <td><div class="col" :title="employee.BankAccount">{{ employee.BankAccount }}</div></td>
                                        <td><div class="col" :title="employee.BankName">{{ employee.BankName }}</div></td>
                                        <td><div class="col" :title="employee.BankPlace">{{ employee.BankPlace }}</div></td>
                                        <td><div class="col" :title="employee.Address">{{ employee.Address }}</div></td>
                                        <td><div class="col" :title="employee.PhoneNumber">{{ employee.PhoneNumber }}</div></td>
                                        <td><div class="col" :title="employee.LandlinePhoneNumber">{{ employee.LandlinePhoneNumber }}</div></td>
                                        <td><div class="col" :title="employee.Email">{{ employee.Email }}</div></td>
                                    </tr>  
                                </tbody>
                            </table>
                        </div>
                        <div class="wrap-button-export">
                            <div class="total-record">
                                <div>Tổng số: <b>{{ employeesTemp.length }}</b> bản ghi</div>
                            </div>
                            <Button class="green" buttonName="Xuất khẩu nhân viên" 
                            @mouseover.native="handleShowTooltip($event, 'Xuất khẩu (Ctrl + S)')" 
                            @mouseout.native="handleHideTooltip()"
                            @click.native="handleExport(employeesTemp)"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import Button from "../../components/base/BaseButton.vue"
import { EventBus } from '../../main';
import moment from "moment"
import axios from "axios";
export default {
    name: 'TheTableExport',
    components: {
        Button,
    },
    props: {
        showExport: {
            type: Boolean,
            default(){
                return false;
            }
        },
        employeesTemp:{
            type: Array,
            default(){
                return [];
            }
        }
    },
    mounted() {
        document.addEventListener("keydown", this.keyDoSave);
        
    },
    beforeDestroy() {
        document.addEventListener("keydown", this.keyDoSave);
        
    },
    methods: {
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
         * Ẩn table
         */
        exitTable(){
            this.$emit('exitTable');
        },

        /**
         * Hiển thị tooltip
         */
        handleShowTooltip(e, contentTooltip){
            let x = e.x - 200;
            console.log(x);
            let y = e.y + 25;
            console.log(y);
            this.$emit('showTooltipModal', y, x, contentTooltip);
        },

        /**
         * Ẩn tooltip
         */
        handleHideTooltip(){
            this.$emit('hideTooltipModal');
        },

        /**
         * Phím tắt Ctrl + S để "Xuất khẩu" dữ liệu
         */
        keyDoSave(e){
            if(this.showExport == true){
                if (!((e.keyCode === 83 && e.ctrlKey)|| e.keyCode === 27)) {
                    return;
                }

                e.preventDefault();
                    //Phím tắt ESC: Tắt bảng export
                    if(e.keyCode === 27){
                        this.exitTable();
                }

                //Phím tắt Ctrl + S : "Xuất khẩu"
                if((e.ctrlKey && e.keyCode === 83)){
                    this.handleExport(this.employeesTemp);
                }
            }
            
        },

        /**
         * Export danh sách nhân viên
         */
        handleExport(exportList){
            if(exportList.length > 0){
                //Hiện loading chờ nhận dữ liệu
                EventBus.$emit('showLoading', true);
                axios.post(`https://localhost:44342/api/v1/Employees/Export`, exportList, {
                    responseType: 'blob',
                })
                .then(res => {
                    console.log(res)
                    console.log(res.data.FileGuid);
                    let blob = new Blob(
                        [res.data], 
                        { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' } ,

                    ),
                    url = window.URL.createObjectURL(blob);
                    window.open(url, '_self');
                })
                .then(()=>{
                    //Export xong thực hiện ẩn table
                    this.$emit('exitTable');
                    //Ẩn loading
                    EventBus.$emit('showLoading', false);
                    EventBus.$emit('showToast', 'Xuất file Excel', 0);
                })
                .catch(err => {
                    console.error(err); 
                })
            }else{
                EventBus.$emit('showPopupWarningExport', true, 6);
            }
        }
    },
}
</script>
<style scoped>
    @import "../../css/layout/tableexport.css";
</style>