<template>
    <Title title="Error..." />
    <div class="container">
        <img src="../../assets/img/error.jpg" alt="Error" />
        <h3>You are experiencing an error</h3>
        <span class="loader"></span>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import Title from '../../components/Title.vue';
    import router from '../../routes/routes.ts';

    export default defineComponent({
        created(): void {
            setTimeout(() => {
                router.push('/');
            }, 5000);
        },
        components: {
            Title
        }
    });
</script>

<style scoped>
    .container {
        text-align: center;
    }

    img {
        margin-top: 25px;
    }
</style>
