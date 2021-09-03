<template>
    <div class="right-bar">
        <TheHeader/>
        <TheContent/>
    </div>
</template>
<script>
import TheHeader from "../layout/TheHeader.vue"
import TheContent from "../layout/TheContent.vue"
export default {
    name: "TheRightBar",
    components:{
        TheHeader,
        TheContent
    }
}
</script>
<style scoped>
    @import "../../css/layout/rightbar.css"
</style>