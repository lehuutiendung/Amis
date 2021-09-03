<template lang="">
    <div class="warning" :class="{'active-warning': emailWarning}">
        <div class="warning-rotate"></div>
        <div class="warning-box">
                <p>Email nhập sai định dạng.</p>
        </div>
    </div>  
</template>
<script>
export default {
    name: 'TheWarning',
    props: {
        emailWarning: {
            type: Boolean,
        },
    }
}
</script>
<style scoped>
    @import "../../css/base/basewarning.css"
</style>