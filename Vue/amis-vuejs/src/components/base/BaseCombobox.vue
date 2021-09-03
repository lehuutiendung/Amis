<template>
    <div>
        <div class="wrap-combobox combobox-room">
            <input type="text" class="combobox" :value="currentPageSize" disabled="disabled"/>
            <div class="combobox-btn" @click="clickChoosePaging()">
                <button>
                    <div class="combobox-img"></div>
                </button>
            </div>
        </div>
        <div class="paging-data" :style="{ display: showPaging }">
            <div class="paging-item" :class="{'paging-item-active' : currentItem == index ? true : false}" v-for="(item, index) in employeeInPage" :key="index" :title="item + ' bản ghi trên 1 trang'" @click="getValuePageSize($event, index)">
                <span>{{ item }}</span> bản ghi trên 1 trang
            </div>
        </div>
  </div>
</template>
<script>

import { EventBus } from "../../main";
export default {
    name: "Combobox",
    props: {
        pagingData: {
            type: Boolean,
            default(){
                return false;
            }
        }
    },
    data() {
        return {
            employeeInPage: [10, 20, 30, 50, 100],
            showPagingData: false, //Mặc định danh sách item chọn số bản ghi bị ẩn
            currentPageSize: '',
            currentItem: 0, //Mặc định active item đầu tiên (10 bản ghi trên 1 trang)
        }
    },
    
    created() {
        this.currentPageSize = this.employeeInPage[0] + ' bản ghi trên 1 trang'
    },

    computed: {
        showPaging() {
            if (this.pagingData) {
                return "block";
            } else {
                return "none";
            }
        },

    },

    methods: {
        //Thay đổi biến ẩn hiện data số nhân viên/trang
        clickChoosePaging() {
            this.$emit('changePagingData');
            this.showPagingData = !this.showPagingData;
        },

        //Lấy giá trị số nhân viên/trang khi Click
        getValuePageSize(e, index){
            //Lấy vị trí item được click
            this.currentItem = index;
            //Lấy giá trị của item
            let value = e.target.innerText;
            let finalValue = "";
            finalValue = value.replace(/\D.*/g, "");
            finalValue = parseInt(finalValue, 10);
            this.showPagingData = !this.showPagingData;
            //Emit cap nhat lai pageSize
            EventBus.$emit("updatePageSize", finalValue);
            // Thay đổi lại số bản ghi trên giao diện
            this.currentPageSize = value; 
            // Emit cap nhat lai pageNumber khi thay doi so nhan vien/trang
            EventBus.$emit("updatePageNumberV2", 1);
            // Ẩn list số bản ghi trên trang
            this.$emit("changePagingData");
        }
    },
}
</script>
<style scoped>
    @import "../../css/base/basecombobox.css"
</style>