<template>
    <div class="wrap-option" :class="{'wrap-option-flex': showOption}" :style="{top:x + 'px',left: y + 'px'}">
        <div class="duplicate-opt mar" @click="replicaClick()">Nhân bản</div>
        <div class="delete-opt mar" @click="deleteClick()">Xóa</div>
        <div class="pause-opt mar" title="Tính năng này đang phát triển">Ngừng sử dụng</div>
    </div>
</template>
<script>
import { EventBus } from '../../main';
export default {
    name: "TheFeatureOption",
    props:{
        x: {
            type: Number,
            default(){
                return 0;
            }
        },
        y: {
            type: Number,
            default(){
                return 0;
            }
        },

        //Hiển thị hộp lựa chọn
        showOption: {
            type: Boolean,
            default(){
                return false;
            }
        },

        employeeCode: {
            type: String,
            default(){
                return "";
            }
        },
    },
    methods: {
        //Bắt sự kiện click nút "Xóa", hiển thị popup xác nhận
        deleteClick(){
            EventBus.$emit('showPopupDelete', this.employeeCode);
        },

        //Bắt sự kiện click nút "Nhân bản", hiển thị form thông tin
        replicaClick(){
            this.$emit('replicaEmployee');
        }
    },
}
</script>
<style scoped>
    @import "../../css/base/basefeatureoption.css";
</style>