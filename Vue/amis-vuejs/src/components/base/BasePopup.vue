<template>
    <!-- Popup cảnh báo hủy form thêm mới-->
  <div class="background-popup" :class="{'popup-active' : showPopup}">
    <div class="popup">
        <div class="mar-popup">
            <div class="content-wrap-popup">
                <div class="warning-popup icon-48">
                    <div class="warning-popup-icon icon-48" v-if="typePopup == 2 || typePopup == 3 || typePopup == 4 || typePopup == 5"></div>
                    <div class="validate-popup-icon icon-48" v-else-if="typePopup == 1 || typePopup == 7"></div>
                    <div class="exclamation-popup-icon icon-48" v-else-if="typePopup == 8"></div>
                    <div class="quest-popup-icon icon-48" v-else></div>
                </div>
                <div class="content-popup" v-if="typePopup == 0">Dữ liệu đã bị thay đổi. Bạn có muốn cất không?</div>
                <div class="content-popup" v-if="typePopup == 1">{{ contentValidate }} không được để trống.</div>
                <div class="content-popup" v-if="typePopup == 2">{{ contentValidate }} vượt quá ngày hiện tại.</div>
                <div class="content-popup" v-if="typePopup == 3">Bạn có thực sự muốn xóa Nhân viên [{{ contentValidate }}] không? </div>
                <div class="content-popup" v-if="typePopup == 4">Mã nhân viên [{{ contentValidate }}] đã tồn tại trong hệ thống, vui lòng kiểm tra lại.</div>
                <div class="content-popup" v-if="typePopup == 5">Bạn có thực sự muốn xóa các Nhân viên: <span v-for="(employeeCode, index) in listEmployeeCode" :key="index">[{{ employeeCode }}], </span>  không? </div>
                <div class="content-popup" v-if="typePopup == 6">Không có dữ liệu để xuất khẩu.</div>
                <div class="content-popup" v-if="typePopup == 7">{{ contentValidate }} nhân viên sai định dạng [NV-], vui lòng kiểm tra lại.</div>
                <div class="content-popup" v-if="typePopup == 8">Có lỗi xảy ra vui lòng liên hệ MISA.</div>
                
            </div>
            <hr>
            <div class="button-wrap" v-if="typePopup == 0">
                <Button class="white" buttonName="Hủy" @click.native="hidePopup()"/>
                <div class="div-flex">
                    <Button class="white" buttonName="Không" @click.native="hidePopupAndForm()"/>
                    <div class="space-10"></div>
                    <Button class="green" buttonName="Có" @click.native="handleClickOk()"/> 
                </div>
            </div>
            <div class="button-wrap type-1" v-if="typePopup == 1 || typePopup == 2 || typePopup == 4 || typePopup == 6 || typePopup == 7 || typePopup == 8">
                <Button class="green" buttonName="Đóng" @click.native="handleClickClose()"/>
            </div>
            <div class="button-wrap type-3" v-if="typePopup == 3">
                <Button class="white" buttonName="Không" @click.native="hidePopup()"/>
                <Button class="green" buttonName="Có" @click.native="acceptDelete()"/>     
            </div>
            <div class="button-wrap type-3" v-if="typePopup == 5">
                <Button class="white" buttonName="Không" @click.native="hidePopup()"/>
                <Button class="green" buttonName="Có" @click.native="acceptDeleteMultiple()"/>     
            </div>
        </div>
    </div>
  </div>
</template>
<script>
import Button from "../../components/base/BaseButton.vue"
import { EventBus } from '../../main';
export default {
    name: 'PopUp',
    components:{
        Button,
    },
    data() {
        return {
            showPopup: false, //Mặc định ẩn popup
            typePopup: 0, // 0: popup hủy 
                          // 1: popup validate 
                          // 2: popup validate ngày
                          // 3: popup xóa 1
                          // 4: popup trùng mã
                          // 5: popup xóa nhiều
                          // 6: popup export (không có dữ liệu để xuất khẩu)
                          // 7: popup sai định dạng mã nhân viên
            contentValidate: '', //Các nội dung validate
            listEmployeeCode: [], //Danh sách các mã employee muốn xóa
        }
    },
    
    mounted() {
        EventBus.$on('showPopupValidate', (content, value, type) => {
            this.contentValidate = content;
            this.showPopup = value;
            this.typePopup = type;
            EventBus.$emit('formatDateBeforeEdit', 4);  //4: trùng mã
        })

        EventBus.$on('showPopupWarning', () => {
            this.showPopup = true;
            this.typePopup = 0;
        })

        //Bắt sự kiện từ BaseFeatureOption, hiển thị popup cảnh báo xóa
        EventBus.$on('showPopupDelete', (value) => {
            this.contentValidate = value;
            this.showPopup = true;
            this.typePopup = 3;
        })

        //Bắt sự kiện từ TheContent, hiển thị popup cảnh báo khi xóa nhiều
        EventBus.$on('showPopupDeleteMultiple', (list, type) => {
            this.listEmployeeCode = list;
            this.showPopup = true;
            this.typePopup = type;
        })

        //Bắt sự kiện từ TheTableExport, hiển thị popup cảnh báo không có dữ liệu để xuất khẩu
        EventBus.$on('showPopupWarningExport', (value, type) => {
            this.showPopup = value;
            this.typePopup = type;
        })

        //Bắt sự kiện từ TheModalBox, hiển thị popup cảnh báo nhập sai định dạng mã nhân viên
        EventBus.$on('showPopupErrorCode', (content, value, type) => {
            this.contentValidate = content;
            this.showPopup = value;
            this.typePopup = type;
        })

        document.addEventListener("keydown", this.keyDoSave);
    },

    beforeDestroy() {
        document.addEventListener("keydown", this.keyDoSave);
        
    },

    methods: {
        //Xử lý sự kiện click nút "Đóng"
        handleClickClose(){
            this.showPopup = !this.showPopup;
            EventBus.$emit('closedPopup');
        },

        /**
         * Xử lý phím tắt (Enter)
         */
        keyDoSave(e){
            if(this.showPopup == true){
                if (!(e.keyCode === 13)) {
                    return;
                }
                e.preventDefault();
                //Phím tắt Enter: Tắt popup (Chỉ áp dụng đối với các popup có 1 button)
                if(e.keyCode === 13){
                    this.handleClickClose();
                }
            }
        },

        //Xử lý sự kiện click nút "Hủy", giữ nguyên các tác vụ khác
        hidePopup(){
            this.showPopup = !this.showPopup;
        },

        //Xử lý sự kiện click nút "Không", và hủy Form
        hidePopupAndForm(){
            this.showPopup = !this.showPopup;
            this.$emit('hidePopupAndForm');
        },
        
        //Xử lý sự kiện đồng ý "Cất"
        handleClickOk(){
            this.showPopup = !this.showPopup;
            EventBus.$emit('agreeSaveData');
        },

        //Xác nhận đồng ý Xóa nhân viên, gửi $emit đến Content thực hiện xóa
        acceptDelete(){
            this.showPopup = !this.showPopup;
            this.$emit('deleteEmployee');
        },

        //Xác nhận đồng ý xóa nhiều nhân viên, gửi $emit đến Content thực hiện xóa nhiều
        acceptDeleteMultiple(){
            this.showPopup = !this.showPopup;
            this.$emit('deleteMultipleEmployee');
        }
    },
}
</script>
<style scoped>
  @import "../../css/base/basepopup.css";  
</style>