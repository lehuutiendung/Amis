<template>
    <div class="footer-table">
        <div class="footer-paginate">
            <div class="total-record">
                <div>Tổng số: <b>{{ formatTotalPages(totalRecord.toString()) }}</b> bản ghi</div>
            </div>
            <div class="paginate-wrap">
                <Combobox :pagingData="pagingData" v-on:changePagingData="changePagingData()" v-click-outside="nameOfCustomEventToCall"/>
                <div class="number-page-wrap">
                    <div class="prev-page" @click="handlePrevPage()">Trước</div>
                    <div class="number-page">
                        <div class="number" v-if="pages[0] > 1" @click="handleNumberPageClick(1)">1</div>
                        <div class="number" v-if="pages[0] > 2">...</div>
                        <div class="number" :class="{'active-number-page': numberClicked == page ? true : false}" v-for="page in pages" :key="page" @click="handleNumberPageClick(page)">{{ page }}</div>
                        <div class="number" v-if="(totalPage > (maxPages + 1)) && (pageNumber < (totalPage - 2))">...</div>
                        <div class="number" v-if="pageNumber < (totalPage - 1)" @click="handleNumberPageClick(totalPage)">{{ totalPage }}</div>
                    </div>
                    <div class="next-page" @click="handleNextPage()">Sau</div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import { EventBus } from '../../main';
import Combobox from "../../components/base/BaseCombobox.vue"
import ClickOutSide from "../../js/detectoutside"
export default {
    name: "TheFooter",
    directives: {
        ClickOutSide
    },
    components: {
        Combobox,
    },
    props: {
        totalRecord: {
            type: Number,
            default(){
                return 0;
            }
        },
        totalPage: {
            type: Number,
            default(){
                return 0;
            }
        },
        maxPages: {
            type: Number,
            default(){
                return 0;
            }
        },
        //Danh sách số trang
        pages: {
            type: Array,
            default(){
                return [];
            }
        },
        pageNumber: {
            type: Number,
            default(){
                return 1;
            }
        }
    },
    data() {
        return {
            numberClicked: 1,  //trang click
            pagingData: false, //Mặc định ẩn danh sách item chọn phân trang
        }
    },

    methods: {
        //Xử lý click vào số trang
        handleNumberPageClick(numberPage){
            this.numberClicked = numberPage;
            EventBus.$emit("updateNumberClick", numberPage);
        },

        //Xử lý click trở về trước 1 trang => Emit tới EmployeeList để phân trang
        handlePrevPage() {
            EventBus.$emit("prevPage");
        },

        //Xử lý click next 1 trang => Emit tới EmployeeList để phân trang
        handleNextPage() {
            EventBus.$emit("nextPage");
        },

        //Thay đổi trạng thái ẩn/hiện của lựa chọn bản ghi - phân trang
        changePagingData(){
            this.pagingData = !this.pagingData;
        },

        //Click ra ngoài dropdown thì ẩn danh sách item
        nameOfCustomEventToCall(){
            this.pagingData = false;
        },

        //Format định dạng số trang
        formatTotalPages(number) {
            var result = "";
            var len = number.length;
            var charCount = 0;
            for (var i = len - 1; i >= 0; i--) {
                result += number[i];
                charCount++;
                if (charCount % 3 == 0) {
                if (charCount == len) break;
                result += ".";
                }
            }
            return result.split("").reverse().join("");
        },
    },
    
    watch: {
        pageNumber: function(){
            this.numberClicked = this.pageNumber;
        }
    }
}
</script>
<style scoped>
    @import "../../css/layout/footer.css"
</style>>
    