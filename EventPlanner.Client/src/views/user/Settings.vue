<template>
    <Title title="Settings" />
    <h6>Page Todos</h6>
    <ul>
        <li>Change Profile Image</li>
        <li>Change Birthday</li>
        <li>Change Password</li>
    </ul>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import Title from '../../compnents/Title.vue';

    interface Data {
        loading: boolean
    }

    export default defineComponent({
        data(): Data {
            return {
                loading: true
            };
        },
        components: {
            Title
        }
    });
</script>

<style scoped>
    h6 {
        text-align: center;
    }

    ul {
        margin: 15px auto;
    }
</style>
