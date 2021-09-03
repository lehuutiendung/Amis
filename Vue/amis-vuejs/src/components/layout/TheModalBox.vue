<template>
    <div class="wrap-modal-box" :style="{ display: modalBoxState }">
        <div class="box-info">
            <div class="title-box div-flex">
                <div class="title-wrap">
                    <div class="title">Thông tin nhân viên</div>
                    <div class="checkbox-text">
                        <input type="checkbox">
                        <div class="text">Là khách hàng</div>
                    </div>
                    <div class="checkbox-text">
                        <input type="checkbox">
                        <div class="text">Là nhà cung cấp</div>
                    </div>
                </div>
                <div class="menu-wrap">
                    <div class="icon-24 help"></div>
                    <div class="icon-24 exit" 
                    @click="exitModalBox()" 
                    @mouseover="handleShowTooltip($event, 'Đóng (ESC)')" 
                    @mouseout="handleHideTooltip()"></div>
                </div>
            </div>
            <div class="wrap-content">
                <div class="wrap-content-body">
                    <div class="two-column-content">
                        <div class="first-column-content">
                            <div class="row-1-first div-flex">
                                <Input class="length-160 row" :required="true" inputName="Mã" 
                                v-model="employee.EmployeeCode" 
                                :tabIndex="1" :maxlength="20" 
                                ref="focusField" 
                                @mouseover.native="handleShowTooltipValidate($event, 'Mã không được để trống.')" 
                                @mouseout.native="handleHideTooltip()"/>
                                <Input :required="true" inputName="Tên" 
                                v-model="employee.FullName" 
                                :employee="employee" :mode="mode" 
                                :tabIndex="2" :maxlength="100"
                                @mouseover.native="handleShowTooltipValidate($event, 'Tên không được để trống.')" 
                                @mouseout.native="handleHideTooltip()"/>
                            </div>
                            <ComboboxForm comboboxName="Đơn vị" :required="true" :dataDepartments="dataDepartments" 
                            :employee="employee" :mode="mode" :tabIndex="3"
                            :employeeReplica="employeeReplica"
                            :replica="replica"
                             v-on:department="getDepartment" 
                             v-on:focusFirstField="focusFirstField()"
                             @click="handleClickCombobox()" 
                             @mouseover.native="handleShowTooltipValidate($event, 'Đơn vị không được để trống.')" 
                             @mouseout.native="handleHideTooltip()"/>
                            <Input inputName="Chức danh" v-model="employee.PositionName" :tabIndex="4" :maxlength="100"/>
                        </div>
                        <div class="second-column-content">
                            <div class="row-1-second div-flex">
                                <Input class="length-160 row" :date="true" 
                                v-model="employee.DateOfBirth" inputName="Ngày sinh" :tabIndex="5"/>
                                <div class="wrap-gender">
                                    <div class="gender">Giới tính</div>
                                    <div class="gender-radio">
                                        <Radio nameRadio="Nam" 
                                        :index="1" v-on:radio="getRadio" :indexRadio="indexRadio"
                                        :employeeReplica="employeeReplica"
                                        :replica="replica" 
                                        :tabIndex="6" :employee="employee"/>
                                        <Radio nameRadio="Nữ" 
                                        :index="0" v-on:radio="getRadio" :indexRadio="indexRadio"
                                        :employeeReplica="employeeReplica"
                                        :replica="replica" 
                                        :tabIndex="7" :employee="employee"/>
                                        <Radio nameRadio="Khác" 
                                        :index="2" v-on:radio="getRadio" :indexRadio="indexRadio"
                                        :employeeReplica="employeeReplica"
                                        :replica="replica" 
                                        :tabIndex="8" :employee="employee"/>
                                    </div>
                                </div>
                            </div>
                            <div class="row-1-second div-flex">
                                <Input class="row" inputName="Số CMND" 
                                v-model="employee.IdentityNumber" :tabIndex="9" :maxlength="25"/>
                                <Input class="length-160" :date="true" 
                                v-model="employee.IdentityDate" inputName="Ngày cấp" :tabIndex="10"/>
                            </div>
                            <Input inputName="Nơi cấp" v-model="employee.IdentityPlace" :tabIndex="11" :maxlength="255"/>
                        </div>
                    </div>
                    <div class="row-content">
                        <Input inputName="Địa chỉ" v-model="employee.Address" :tabIndex="12" :maxlength="255"/>
                        <div class="row-content-1 div-flex">
                            <Input class="length-197 row" inputName="ĐT di động" 
                            v-model="employee.PhoneNumber" 
                            :tabIndex="13" :maxlength="50"/>
                            <Input class="length-197 row" inputName="ĐT cố định" 
                            v-model="employee.LandlinePhoneNumber" 
                            :tabIndex="14" :maxlength="50"/>
                            <Input class="length-240 row" inputName="Email" 
                            v-model="employee.Email" 
                            :tabIndex="15" 
                            @blur="validateEmail($event, employee.Email)" 
                            @input="handleInputEmail()" :maxlength="100"/>
                            <Warning :emailWarning="emailWarning"/>
                        </div>
                        <div class="row-content-2 div-flex">
                            <Input class="length-197 row" inputName="Tài khoản ngân hàng" 
                            v-model="employee.BankAccount" :tabIndex="16" :maxlength="25"/>
                            <Input class="length-197 row" inputName="Tên ngân hàng" 
                            v-model="employee.BankName" :tabIndex="17" :maxlength="255"/>
                            <Input class="length-240 row" inputName="Chi nhánh" 
                            v-model="employee.BankPlace" :tabIndex="18" @blur="rollBackTabindex()" :maxlength="255"/>
                        </div>
                    </div>
                </div>
                <hr>
                <div class="wrap-button div-flex">
                    <Button class="white" buttonName="Hủy" @click.native="exitModalBox()"/>
                    <div class="wrap-button-save div-flex">
                        <Button class="white tooltip" buttonName="Cất" 
                        @click.native="handleClickSave()" 
                        @mouseover.native="handleShowTooltip($event, 'Cất (Ctrl + S)')" 
                        @mouseout.native="handleHideTooltip()"/>
                        <div class="space-10"></div>
                        <Button class="green" buttonName="Cất và Thêm" 
                        @click.native="handleClickSaveAndAdd()" 
                        @mouseover.native="handleShowTooltip($event, 'Cất và thêm (Ctrl + Shift + S)')" 
                        @mouseout.native="handleHideTooltip()"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import axios from "axios";
import { EventBus } from '../../main';
import moment from "moment"
import Input from "../../components/base/BaseInput.vue"
import ComboboxForm from "../../components/base/BaseComboboxForm.vue"
import Button from "../../components/base/BaseButton.vue"
import Radio from "../../components/base/BaseRadio.vue"
import Warning from "../../components/base/BaseWarning.vue"
export default {
    name: "TheModalBox",
    components: {
        Input,
        ComboboxForm,
        Button,
        Radio,
        Warning,
    },

    props: {
        modalBoxShow: {
            type: Boolean,
            default(){
                return false;
            }
        },
        mode: {
            type: Number,
            default(){
                return 2; // 0 - Thêm mới, 1 - Sửa
            }, 
            required: true,
        },
        employeeId: {
            type: String,
            default(){
                return "";
            },
            required: true,
        },
        //Object chứa dữ liệu của bản ghi nhân bản
        employeeReplica: {
            type: Object,
            default(){
                return {};
            }
        },
        replica: {
            type: Boolean,
            default(){
                return false;
            }
        }
    },

    data() {
        return {
            inputName: "", //Label của component input,
            employee: {},
            dataDepartments: [],
            department: "11452b0c-768e-5ff7-0d63-eeb1d8ed8cef", //Id của Phòng tài chính đầu tư
            departmentName: "", //Tên của đơn vị
            indexRadio: 1, //Mặc định radio đầu tiên
            validateSave: true, //Validate khi lưu
            emailWarning: false, //Validate định dạng email, mặc định chưa nhập là false
        }
    },
    created() {
        // this.employee.DepartmentId = this.department;
    },
    mounted() {
        axios.get(`https://localhost:44342/api/v1/Departments`)
        .then(res => {
            this.dataDepartments = res.data;
        })
        .catch(err => {
            console.error(err); 
        })
        
        //Nhận sự kiện đồng ý "Cất" dữ liệu từ Popup
        EventBus.$on('agreeSaveData', () => {
            this.handleClickSave();
        })

        //Ẩn cảnh báo trùng mã khi Edit và format lại ngày tháng
        EventBus.$on('formatDateBeforeEdit', (val) => {
            if(val == 4){
                this.employee.DateOfBirth = this.formatDate(this.employee.DateOfBirth);
                this.employee.IdentityDate = this.formatDate(this.employee.IdentityDate);
            }
        })
           
        document.addEventListener("keydown", this.keyDoSave);
        
    },

    beforeDestroy() {
        document.removeEventListener("keydown", this.keyDoSave);
    },

    computed: {
        modalBoxState() {
            if (this.modalBoxShow) {
                return "flex";
            } else {
                return "none";
            }
        },
    },

    methods: {
        //Ẩn modalbox
        exitModalBox(){
            if(Object.keys(this.employee).length > 1){
                EventBus.$emit("showPopupWarning");
            }else{
                this.$emit("exitModalBox");
            }
        },

        //Hiển thị tooltip khi các trường nhập bắt buộc nhập liệu thiếu
        handleShowTooltipValidate(e, contentTooltip){
            if(e.target.value == ''){
                let x = e.x - 200;
                let y = e.y + 25;
                this.$emit('showTooltipModal', y, x, contentTooltip);
            }
        },

        //Hiển thị tooltip
        handleShowTooltip(e, contentTooltip){
            let x = e.x - 200;
            let y = e.y + 25;
            this.$emit('showTooltipModal', y, x, contentTooltip);
        },

        //Ẩn tooltip
        handleHideTooltip(){
            this.$emit('hideTooltipModal');
        },

        //Lấy giá trị của combobox đơn vị
        getDepartment(value, name){
            this.$set(this.employee, "DepartmentId", value);
            this.department = value;
            this.departmentName = name;
        },

        //Xử lý sự kiện click combobox
        handleClickCombobox(){
            this.showDataDepartment = !this.showDataDepartment;
        },

        //Thay đổi giá trị và gán giá trị cho Gender của employee
        getRadio(value){ 
            this.$set(this.employee, "Gender", value);
            this.indexRadio = value;
        },

        //Đóng popup, thực hiện focus đến các trường chưa nhập
        focusFormAfterValidate(){
            EventBus.$on('closedPopup', ()=> {
                var list = this.$el.querySelectorAll(`input.required`);
                for(let i = 0; i < list.length; i++){
                    if(list[i].value == ''){
                        list[i].focus();
                        return false;
                    }
                }
            })
        },

        //Validate các trường bắt buộc nhập khi lưu dữ liệu
        validateRequired(){
            var list = this.$el.querySelectorAll(`input.required`);
            list.forEach(item => {
                //Thay đổi border của các trường bắt buộc khi cất
                if(item.value == ''){
                    if(item.classList.contains('combobox-form')){
                        item.parentElement.style.border = "1px solid #FF4747";
                    }
                    else item.style.border = "1px solid #FF4747";
                }
            });
            //Check các trường bắt buộc nhập và hiển thị popup
            for(let i = 0; i < list.length; i++){
                if(list[i].value == ''){
                    if(i == 0){
                        EventBus.$emit("showPopupValidate", 'Mã', true, 1);
                    }else if(i == 1){
                        EventBus.$emit("showPopupValidate", 'Tên', true, 1);
                    }else{
                        EventBus.$emit("showPopupValidate", 'Đơn vị', true, 1);
                    }
                    this.focusFormAfterValidate();
                    return false;
                }
                if(!list[0].value.includes('NV-', 0)){
                    EventBus.$emit("showPopupErrorCode", 'Mã', true, 7);
                    console.log(list[0].value);
                    return false;
                }
            }
            
            return true;
        },

        //Validate Email
        validateEmail(e, value) {
            if(value){
                var patternEmail =
                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                if (!patternEmail.test(value)) {
                    e.target.style.border = "1px solid #FF4747";
                    this.emailWarning = true;
                    return true;
                } else {
                    console.log("Email OK!");
                    this.emailWarning = false;
                    return false;
                }
            }
        },
        //Validate ngày tháng (không được quá ngày hiện tại)
        validateDateTime(paramDate){
            let isoDate = moment(paramDate,'DD/MM/YYYY').format('YYYY-MM-DD[T]HH:mm:ss');
            let isoNow = moment(new Date()).format('YYYY-MM-DD[T]HH:mm:ss');
            if(isoDate > isoNow){
                EventBus.$emit("showPopupValidate", 'Ngày sinh', true, 2);
                return false;
            }
            return true;
        },  

        /**
         * Ẩn warning khi thực hiện nhập email
         */
        handleInputEmail(){
            this.emailWarning = false;
        },

        /**
         * Hàm xử lý chung cho thêm mới, chỉnh sửa nhân viên
         */
        handleSaveCommon(){
            //Set mặc định khi chưa chọn đơn vị sẽ lấy department là "Phòng Đào Tạo"
            this.employee.DepartmentId = this.department;
            //Set mặc định cho giới tính là "Nam"
            if(this.replica == true){
                this.employee.Gender = this.employeeReplica.Gender;
            }else{
                this.employee.Gender = this.indexRadio;
            }
            let dateOfBirth = this.employee.DateOfBirth;
            let identityDate = this.employee.IdentityDate;
            if(this.validateRequired()){
                //Kiểm tra nếu có ngày sinh thì format theo chuẩn ISO
                if(dateOfBirth){
                    if(this.validateDateTime(dateOfBirth)){
                        this.employee.DateOfBirth = moment(dateOfBirth,'DD/MM/YYYY').format('YYYY-MM-DD[T]HH:mm:ss');
                    }else{
                        this.employee.DateOfBirth = null;
                        return false;
                    }
                }
                //Kiểm tra nếu có ngày cấp CMND thì format theo chuẩn ISO
                if(identityDate){
                    if(this.validateDateTime(identityDate)){
                        this.employee.IdentityDate = moment(identityDate,'DD/MM/YYYY').format('YYYY-MM-DD[T]HH:mm:ss');
                    }else{
                        this.employee.IdentityDate = null;
                        return false;
                    }
                }
                return true;
            }else return false;
        },

        /**
         * Xử lý sự kiện click nút "Cất"
         */
        handleClickSave(){
            if(this.mode == 0){
                //Thực hiện với form thêm mới
                if(this.handleSaveCommon()){
                    this.addEmployee(this.employee);
                }
            }else{
                //Thực hiện form ở chế độ sửa thông tin
                if(this.handleSaveCommon()){
                    this.editEmployee(this.employee, this.employeeId);
                }
            }
            
        },

        /**
         * Phím tắt Ctrl + S để "Cất" dữ liệu
         */
        keyDoSave(e){
            if (!((e.keyCode === 83 && e.ctrlKey)|| e.keyCode === 27)) {
                return;
            }
            e.preventDefault();
            if(this.mode != 2){
                //Phím tắt ESC: Tắt form
                if(e.keyCode === 27){
                    this.exitModalBox();
                }

                //Phím tắt Ctrl + Shift + S : "Cất" và "Thêm"
                if((e.ctrlKey && e.shiftKey && e.keyCode === 83)){
                    this.handleClickSaveAndAdd();
                }else if((e.keyCode === 83 && e.ctrlKey)){
                    //Phím tắt Ctrl + S : "Cất"
                    this.handleClickSave();
                }
            }
        },

        /**
         * Xử lý sự kiện click nút "Cất và Thêm"
         */
        handleClickSaveAndAdd(){
            if(this.mode == 0){
                //Thực hiện khi form là thêm mới
                if(this.handleSaveCommon()){

                    this.addEmployeeAndHold(this.employee);
                }
            }else{
                //Thực hiện khi form ở chế độ sửa thông tin
                if(this.handleSaveCommon()){

                    this.editEmployeeAndHold(this.employee, this.employeeId);
                }
            }
            
        },

        /**
         * Gọi API thực hiện thêm mới, sau khi thêm mới, thực hiện ẩn form
         */
        addEmployee(employee){
            axios
              .post(`https://localhost:44342/api/v1/Employees`, employee)
              .then((res) => {
                console.log(res);
                this.employee = {};
                //Cật nhật lại bảng
                this.$emit("updateTable");
              }).then(() => {
                EventBus.$emit("showToast", 'Thêm mới', 0);
              })
              .catch((err) => {
                console.log(err);
                //Xử lý với các mã trả về
                switch(err.response.status){  
                case 400: 
                        EventBus.$emit("showPopupValidate", 'Mã', true, 4);
                        break;
                }        
              });
        },

        /**
         * Gọi API thực hiện thêm mới, sau khi thêm mới giữ lại form
         */
        addEmployeeAndHold(employee){
            axios
                .post(`https://localhost:44342/api/v1/Employees`, employee)
                .then((res) => {
                    console.log(res);
                }).then(() => {
                    this.$emit("updateTableAndHold");
                    EventBus.$emit("showToast", 'Thêm mới', 0);
                    this.callNewEmployeeCode();
                })
                .catch((err) => {
                    console.log(err);
                    //Xử lý với các mã trả về
                    switch(err.response.status){  
                        case 400: 
                                EventBus.$emit("showPopupValidate", 'Mã', true, 4);
                                break;
                    }
                });
        },

        /**
         * Gọi API sinh mã nhân viên
         */
        callNewEmployeeCode(){
            //Hiện loading chờ nhận dữ liệu
            EventBus.$emit('showLoading', true);

            this.employee = {};
            axios
                .get("https://localhost:44342/api/v1/Employees/NewCode")
                .then((res) => {
                    this.$set(this.employee, "EmployeeCode", res.data.EmployeeCode );
                }).then(() => {
                    EventBus.$emit('showLoading', false);
                })
                .catch((err) => {
                    console.error(err);
                    if(!err.response){
                        EventBus.$emit("showPopupWarningExport", true, 8);
                        EventBus.$emit('showLoading', false);
                    }
                });
        },

        /**
         * Gọi API thực hiện sửa nhân viên, ẩn form sau khi sửa
         */
        editEmployee(){
            axios
              .put(
                `https://localhost:44342/api/v1/Employees/${this.employeeId}`,
                this.employee
              )
              .then((res) => {
                console.log(res);
                //Cật nhật lại bảng
                this.$emit("updateEditTable");
                EventBus.$emit("showToast", 'Chỉnh sửa', 0);
              })
              .catch((err) => {
                    console.log(err);
                //Xử lý với các mã trả về
                    switch(err.response.status){  
                        case 400: 
                                EventBus.$emit("showPopupValidate", this.employee.EmployeeCode , true, 4);
                                break;
                    }
              });
        },

        /**
         * Gọi API thực hiện sửa nhân viên, giữ form sau khi sửa
         */
        editEmployeeAndHold(){
            axios
              .put(
                `https://localhost:44342/api/v1/Employees/${this.employeeId}`,
                this.employee
              )
              .then((res) => {
                console.log(res);
              }).then(() => {
                this.$emit("updateEditTableAndHold");
                EventBus.$emit("showToast", 'Chỉnh sửa', 0);
                this.callNewEmployeeCode();
              })
              .catch((err) => {
                    console.log(err);
                //Xử lý với các mã trả về
                    switch(err.response.status){  
                        case 400: 
                                EventBus.$emit("showPopupValidate", 'Mã', true, 4);
                                break;
                    }
              });
        },

        /**
         * Format ngày tháng hiển thị trên form
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
         * Focus ô đầu tiên của form
         */
        focusFirstField(){
            //Mở form tự động focus vào ô đầu tiên
            this.$refs.focusField.$el.children[1].focus();
        },

        /**
         * Sau khi blur khỏi input cuối cùng của form, rollback focus về input đâu tiên
         */
        rollBackTabindex(){
            this.focusFirstField();
        }
    },

    watch: {
        mode: function () {
            let vm = this;
            // Gọi API lấy thông tin chi tiết của 1 nhân viên

            // Nếu mode = 1: Form sửa => Gọi API
            if(this.mode == 1){
                //Hiện loading chờ nhận dữ liệu
                EventBus.$emit('showLoading', true);
                axios
                .get(`https://localhost:44342/api/v1/Employees/${this.employeeId}`)
                .then((res) => {
                    vm.employee = res.data;
                    //Format định dạng ngày tháng trước khi hiển thị lên form thông tin
                    if(vm.employee.DateOfBirth){
                        vm.employee.DateOfBirth = this.formatDate(res.data.DateOfBirth);
                    }
                    if(vm.employee.IdentityDate){
                        vm.employee.IdentityDate = this.formatDate(res.data.IdentityDate);
                    }
                    //Active input-radio giới tính ứng với dữ liệu nhận được
                    if(res.data.Gender == null){
                        this.indexRadio = 2;
                    }
                    else this.indexRadio = res.data.Gender;
                    EventBus.$emit('activeCircle');

                })
                .then(()=>{
                    //Ẩn loading
                    EventBus.$emit('showLoading', false);
                })
                .catch((err) => {
                    console.error(err);
                });
            }else if (this.mode == 0) {
            // Nếu mode = 0: Form thêm mới => Clear dữ liệu cũ
                this.callNewEmployeeCode();
                this.indexRadio = 1;
                EventBus.$emit('activeCircle');
            }else{
                this.employee = {};
            }
        }, 

        modalBoxShow: function(){
            if(this.replica == true){
                this.employee = this.employeeReplica;
                // this.department = this.employeeReplica.DepartmentId;
                // this.departmentName = this.employeeReplica.DepartmentName;
                //Format định dạng ngày tháng trước khi hiển thị lên form thông tin
                if(this.employee.DateOfBirth){
                    this.employee.DateOfBirth = this.formatDate(this.employeeReplica.DateOfBirth);
                }
                if(this.employee.IdentityDate){
                    this.employee.IdentityDate = this.formatDate(this.employeeReplica.IdentityDate);
                }
                
            }else {
                this.employee = {};
            }
            //Sau khi form được mở lại, đặt lại border cho các trường required
            this.$el.querySelectorAll(`div.wrap-combobox`)[0].style.border = "1px solid #bbbbbb";
            this.$el.querySelectorAll(`input`).forEach(element => {
                element.style.border = "1px solid #bbbbbb";
                this.$el.querySelectorAll(`input.combobox-form`)[0].style.border = "none";
            });
            this.$nextTick(() =>  {
                if(this.modalBoxShow == true){
                    //Mở form tự động focus vào ô đầu tiên
                    this.focusFirstField();
                }
            })
        },

    }
}
</script>
<style scoped>
    @import "../../css/layout/modalbox.css";
</style>