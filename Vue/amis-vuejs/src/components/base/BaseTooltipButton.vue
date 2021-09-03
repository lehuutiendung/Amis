<template>
    <div class="wrap-tooltip" :class="{'show-tooltip': showTooltip}" :style="{top:xTooltip + 'px',left: yTooltip + 'px'}">
        <div class="tooltip">{{ contentTooltip }}</div>
    </div>
</template>
<script>
export default {
    name: "TooltipButton",
    props:{
        xTooltip: {
            type: Number,
            default(){
                return 0;
            }
        },
        yTooltip: {
            type: Number,
            default(){
                return 0;
            }
        },

        contentTooltip: {
            type: String,
            default(){
                return "";
            }
        },

        //Hiển thị tooltip
        showTooltip: {
            type: Boolean,
            default(){
                return false;
            }
        }
    }
}
</script>
<style scoped>
    @import "../../css/base/basetooltip.css";
</style>