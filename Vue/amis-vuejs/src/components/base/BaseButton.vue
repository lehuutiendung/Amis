<template>
    <div class="button-add" v-show="show">
        <div class="button-add-text">{{ buttonName }}</div>
    </div>
</template>
<script>
export default {
    name: "Button",
    props: {
        buttonName:{
            type: String,
            default(){
                return '';
            }
        },
        //Mặc định các button được hiển thị
        show:{
            type:Boolean,
            default(){
                return true;
            }
        }
    }
}
</script>
<style scoped>
    @import "../../css/base/basebutton.css";
</style>