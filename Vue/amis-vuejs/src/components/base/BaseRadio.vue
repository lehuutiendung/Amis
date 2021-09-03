<template>
    <div class="gender-radio-wrap">
        <input class="radio-input" type="radio" >
        <span class="radio-wrap" :tabIndex="tabIndex" @focus="handleFocusRadio()" @blur="handleBlurRadio()" @keyup.enter="handleEnterRadio()" @click="handleClickRadio()">
            <span class="radio-border"></span>
            <span class="radio-border-big" :class="{'active-border-radio' : activeBorder }"></span>
            <span class="radio-circle" :class="{'active-circle-radio' : chooseItem }"></span>
        </span>
        <div class="name-radio">{{ nameRadio }}</div>
    </div>
</template>
<script>
import { EventBus } from '../../main';
export default {
    name: "Radio",
    props:{
        nameRadio: {
            type: String,
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
        //Index của từng radio
        index: {
            type: Number,
            default(){
                return 0;
            }
        },

        //Index của Radio được chọn
        indexRadio: {
            type: Number,
            default(){
                return 1;
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
    },

    mounted() {
        EventBus.$on('activeCircle', () => {
            this.activeCircle = true;
        })
    },

    data() {
        return {
            activeBorder: false, //Mặc định chưa hiện border focus
            activeCircle: false, //Mặc định chọn item đầu tiên
        }
    },
    
    computed: {
        chooseItem: function(){
            if(this.replica == false){
                if(this.activeCircle && this.index == this.indexRadio){
                    return true;
                }else{
                    return false;
                }
            }else{
                if(this.activeCircle && this.index == this.employeeReplica.Gender){
                    return true;
                }else{
                    return false;
                }
            }
        }
    },

    methods: {
        //Focus vào radio, hiển thị border
        handleFocusRadio(){
            this.activeBorder = true;
        },

        //Blur ra ngoài radio, ẩn border
        handleBlurRadio(){
            this.activeBorder = false;
        },

        //Bắt sự kiện phím enter chọn radio
        handleEnterRadio(){
            this.activeCircle = true;
            this.$emit('radio', this.index);
        },

        //Bắt sự kiện click radio
        handleClickRadio(){
            this.activeCircle = true;
            this.$emit('radio', this.index);
        }

    },

}
</script>
<style scoped>
    @import "../../css/base/baseradio.css"
</style>