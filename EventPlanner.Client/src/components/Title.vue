<template>
    <h1>{{ title }}</h1>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    export default defineComponent({
        props: {
            title: {
                type: String,
                required: true
            }
        }
    });
</script>

<style scoped>
    h1 {
        width: 100%;
        text-align: center;
        padding: 15px 0;
        background-color: lightblue;
        color: black;
        margin: 0 0 25px 0;
    }
</style>
