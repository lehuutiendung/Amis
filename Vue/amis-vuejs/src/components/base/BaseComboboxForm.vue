<template>
    <div class="das-combobox">
        <div class="combobox-text">{{comboboxName}}<span>&nbsp;*</span></div>
        <div class="wrap-combobox combobox-room" ref="comboboxForm" >
            <input type="text" class="combobox-form" :class="{'required': required}" 
            v-model="departmentName" v-on="inputListeners" 
            @input="handleInputCombobox($event)" 
            :tabIndex="tabIndex" 
            @blur="handleBlur($event)" 
            @focus="handleFocus($event)" 
            @keydown="handleKeyDown($event)"/>
            <!-- @mousedown="preventClick($event)" -->
            <div class="combobox-btn-form" @click.stop="showDataCombobox()">
                <button>
                    <div class="combobox-img"></div>
                </button>
            </div>
        </div>
        <ul class="combobox-data-form" :class="{'showCombobox' : showData}" @keydown="handleKeyDown($event)">
            <!-- Binding data here! -->
            <div class="title-data">
                <div class="title-code-data">Mã đơn vị</div>
                <div class="title-name-data">Tên đơn vị</div>
            </div>
            <li class="combobox-form-item" :class="{'item-background-choose' : index == current ? true:false}" v-for="(department, index) in listDepartments" :key="index" @click="handleItemComboboxClick(department)">
                <div class="content-code" :class="{'item-color-choose' : index == current ? true:false }" >{{ department.DepartmentCode }}</div>
                <div class="content-name" :class="{'item-color-choose' : index == current ? true:false }">{{ department.DepartmentName }}</div>
            </li>
        </ul>
    </div>
</template>
<script>
import { EventBus } from '../../main';
export default {
    name: "ComboboxForm",
    props: {
        comboboxName: {
            type: String,
            default(){
                return "";
            }
        },
        //Dữ liệu tất cả đơn vị
        dataDepartments: {
            type: Array,
            default(){
                return "";
            }
        },
        //tabindex
        tabIndex: {
            type: Number,
            default(){
                return 1;
            }
        },
        
        //Bắt buộc nhập
        required: {
            type: Boolean,
            default(){
                return false;
            }
        },

        employee: {
            type: Object,
            default(){
                return {};
            }
        },

        employeeReplica:{
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
        },  

        mode: {
            type: Number,
            default: 2, // 0 - Thêm mới, 1 - Sửa
            required: true,
        },

    },
    data() {
        return {
            showData: false, //Mặc định list data bị ẩn,
            departmentName: 'Phòng Tài Chính Đầu Tư', //Tên của đơn vị, mặc định vào form set giá trị "Phòng đào tạo"
            departmentId: '11452b0c-768e-5ff7-0d63-eeb1d8ed8cef', //Id của đơn vị, mặc định vào form set giá trị "Phòng đào tạo"
            listDepartments: [], //Danh sách chứa các item để bind dữ liệu, và thay đổi khi thực hiện tìm kiếm trong combobox
            current: 0,    //Item đã click
            typeClick: false, //Kiểu chọn item bằng click
        }
    },

    created() {
        this.listDepartments = this.dataDepartments;
    },
    
    mounted() {
        //Thay đổi border input search
        EventBus.$on('hideComboboxForm', () => {
            if(this.showData == true && this.typeClick == true){
                this.showData = false;
                this.typeClick = false;
            }
        })

    },
    computed: {
        inputListeners: function () {
            var vm = this
            return Object.assign({},
            this.$listeners,
            {
                input: function (event) {
                vm.$emit('input', event.target.value)
                }
            }
            )
        },

    },
    methods: {
        //Thay đổi ẩn hiện combobox
        showDataCombobox(){
            this.showData = !this.showData;
        },

        /**
         * Xử lý sự kiện chọn item 
         */
        handleItemComboboxClick(department){
            //Cập nhật lại danh sách item
            this.typeClick = true;
            this.listDepartments = this.dataDepartments;
            this.departmentName = department.DepartmentName;
            this.departmentId = department.DepartmentId;
            console.log(department.DepartmentId, department.DepartmentName);
            this.$emit("department", department.DepartmentId, department.DepartmentName);
        },

        /**
         * Xử lý sự kiện input trong combobox
         */
        handleInputCombobox(e){
            this.showData = true;
            let valueSearch = e.target.value;
            this.listDepartments = [];
            this.dataDepartments.forEach(item => {
                if(item.DepartmentName.toLowerCase().includes(valueSearch.toLowerCase())){
                    this.listDepartments.push(item)
                }
            });
        },

        //Ngăn chặn xảy ra sự kiện focus khi có sự kiện click lan tỏa
        preventClick(e){
            e.preventDefault();
        },

        //Xử lý sự kiện blur input
        handleBlur(e){
            this.showData = false;
            if (e.target.value == "" && this.required == true) {
                e.target.parentElement.style.border = "1px solid #FF4747";
            } else {
                e.target.parentElement.style.border = "1px solid #bbbbbb";
            }
        },
        
        //Xử lý sự kiện focus input
        handleFocus(e){
            e.stopPropagation();
            this.showData = true;
            e.target.parentElement.style.border = "1px solid #019160";
        },

        //Sự kiện keydown phím mũi tên xuống (keyCode: 40), phím mũi tên lên (keyCode: 38), phím enter chọn phần tử (keyCode: 13)
        handleKeyDown(e){
            if(e.keyCode == 40){
                if(this.current < (this.listDepartments.length - 1)){
                    this.current++;
                }
            }
            if(e.keyCode == 38){
                if(this.current > 0){
                    this.current--;
                }
            }
            if(e.keyCode == 13){
                this.showData = !this.showData;
                for(let i = 0; i < this.listDepartments.length; i++){
                    if(this.current == i){
                        this.departmentName = this.listDepartments[i].DepartmentName;
                        this.departmentId = this.listDepartments[i].DepartmentId;
                        this.$emit("department", this.departmentId, this.departmentName);
                    }
                }
            }
        }
    },

    watch: {
        departmentName: function(){
            this.$emit('department', this.departmentId , this.departmentName);
        },
        dataDepartments: function(){
            this.listDepartments = this.dataDepartments;
        },
        employee: function () {
            if(this.mode == 1){
                //Thay đổi lại giá trị của department sau khi thay đổi object employee
                this.departmentName = this.employee.DepartmentName;
                this.departmentId = this.employee.DepartmentId;
            }else if(this.mode == 2){
                this.departmentName = this.listDepartments[0].DepartmentName;
                this.departmentId = this.listDepartments[0].DepartmentId;
                this.current = 0;
            }
        },
        'employee.EmployeeCode': function(){
            this.$emit('focusFirstField');
            if(this.mode == 0){
                this.current = 0;
                for(let i = 0; i < this.listDepartments.length; i++){
                    if(this.current == i){
                        if(this.replica == true){
                            this.departmentName = this.employeeReplica.DepartmentName;
                            this.departmentId = this.employeeReplica.DepartmentId;
                        }else{
                            this.departmentName = this.listDepartments[i].DepartmentName;
                            this.departmentId = this.listDepartments[i].DepartmentId;
                            this.$emit("department", this.departmentId, this.departmentName);
                        }
                    }
                }
                this.showData = false;
            }
        },
        showData: function(){
            if(this.showData){
                this.$refs.comboboxForm.style.border = "1px solid #019160";
            }
        }
    }
}
</script>
<style scoped>
    @import "../../css/base/basecomboboxform.css";
</style>