<template>
    <Title title="Welcome to the Event Planner!" />
    <div class="container">

    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import Title from '../components/Title.vue';

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

</style>
