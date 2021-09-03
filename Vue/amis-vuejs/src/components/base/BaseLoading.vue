<template lang="">
    <div class="wrap-loading" :class="{'show-loading': showLoading || showLoadingData }">
        <div class="position-icon-loading">
            <div class="icon-32 icon-loading"></div>
        </div>
    </div>
</template>
<script>
import { EventBus } from '../../main';
export default {
    name: 'TheLoading',
    props: {
        showLoading: {
            typeo: Boolean,
            default(){
                return false;
            }
        }
    },
    data() {
        return {
            showLoadingData: false, //Sử dụng thay đổi với EventBus
        }
    },
    mounted() {
        EventBus.$on('showLoading', (value) => {
            this.showLoadingData = value;
        })
    },
}
</script>
<style scoped>
    @import "../../css/base/baseloading.css";
</style>