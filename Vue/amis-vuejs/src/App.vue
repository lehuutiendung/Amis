<template>
  <div id="app" @click="handleEveryWhereClick()">
      <TheMenu/>
      <TheRightBar/>
  </div>
</template>

<script>
import { EventBus } from '../src/main';
import TheMenu from "../src/components/layout/TheMenu.vue"
import TheRightBar from "../src/components/layout/TheRightBar.vue"
export default {
  name: 'App',
  components: {
    TheMenu,
    TheRightBar,
  },
  methods: {
    handleEveryWhereClick(){
      //Ẩn list lựa chọn chức năng
      EventBus.$emit("hideFeatureOption");
      //Thay đổi border input search
      EventBus.$emit("changeBoderInput");
      //Ẩn list combobox trong form
      EventBus.$emit("hideComboboxForm");

    }
  },
}
</script>

<style>
@import "../src/css/main.css"
</style>
