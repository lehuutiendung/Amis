<template>
    <div class="wrap-input-text">
        <!-- Label của input -->
        <div class="text-input">{{ inputName }}<span v-if="required">&nbsp;*</span></div>
        <div class="custome-input-date" v-if="date">
            <v2-datepicker class="datepicker" 
                format="DD/MM/YYYY" 
                :lang="lang" 
                :customLocals="locals"
                :picker-options="datePickerOptions" 
                placeholder="DD/MM/YYYY" 
                v-on:input="inputDatePicker($event)" 
                ref="datePicker">
            </v2-datepicker>
            <input class="custome-date" type="text" placeholder="DD/MM/YYYY" 
            v-bind:value="value" 
            :tabIndex="tabIndex" 
            v-on="inputListeners" 
            v-on:focus="handleInput($event)" 
            v-on:blur="handleBlur($event)">
        </div>
        <input type="text" 
        :class="{'required': required}"
        :tabIndex="tabIndex"
        :maxlength="maxlength"
        ref="inputRequired" 
        v-bind:value="value" 
        v-on="inputListeners" 
        v-on:focus="handleInput($event)"
        v-on:blur="handleBlur($event)" v-else>
    </div>
</template>

<script>
import locals from "../../js/locales"
import moment from "moment"
export default {
    name: "Input",
    props: {
        value:{
            type: String,
            default(){
                return "";
            }
        },

        //Label của input
        inputName: {
            type: String,
            default(){
                return "";
            }
        },

        //Bắt buộc nhập
        required: {
            type: Boolean,
            default(){
                return false;
            }
        },

        normal: {
            type: Boolean,
            default(){
                return true;
            }
        },

        //Input dạng date
        date: {
            type: Boolean,
            default(){
                return false;
            },
        },

        //tabindex
        tabIndex: {
            type: Number,
            default(){
                return 1;
            }
        },

        //Độ dài tối đa nhập input
        maxlength: {
            type: Number,
            default(){
                return 10;
            }
        },  

        //Object chứa nhân viên
        employee: {
            type: Object,
            default(){
                return {};
            }
        },
        //Chế độ của form, mặc định form ẩn (mode 2)
        mode:{
            type: Number,
            default(){
                return 2;
            }
        }
    },
    data(){
        return {
            lang: 'it',
            locals,
            //Max date là ngày hiện tại
            datePickerOptions: {
                  disabledDate (date) {
                        return date > new Date();
                 }
            },
        }
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
        //Format định dạng ngày tháng
        inputDatePicker(e){
            this.$emit('input', moment(e).format('DD/MM/YYYY'));
        },
        
        //Bắt sự kiện focus input thay đổi border
        handleInput(e) {
            if(this.date){
                //Border các input thường
                e.target.style.border = "1px solid #019160";
                //Border của input custome
                e.target.style.borderRight = "none";
                this.$refs.datePicker.$el.style.border = "1px solid #019160";
            }else{
                e.target.style.border = "1px solid #019160";
            }
        },

        /**
         * Bắt sự kiện blur, validate cho input
         */
        handleBlur(e) {
            if (e.target.value == "" && this.required == true) {
                e.target.style.border = "1px solid #FF4747";
            }
            else {
                //Border các input thường
                e.target.style.border = "1px solid #bbbbbb";
                //Border của input custome
                if(this.date){
                    this.$el.querySelectorAll(`input.custome-date`)[0].style.borderRight = "none";
                    this.$refs.datePicker.$el.style.border = "1px solid #bbbbbb";
                }     
            }
        },
    },
    watch: {
        'employee.EmployeeCode': function(){
            if(this.mode == 0){
                this.$refs.inputRequired.style.border = "1px solid #bbbbbb";
            }
        }
    }
}

</script>
<style scoped>
    @import "../../css/base/baseinput.css"
</style>