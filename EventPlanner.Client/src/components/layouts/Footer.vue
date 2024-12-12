<template>
    <footer>
        <small>&#169; Copyright {{ year }} Jonathan Cisneros, All Rights Reserved.</small>
    </footer>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    interface Data {
        year: null | number
    }

    export default defineComponent({
        data(): Data {
            return {
                year: null
            };
        },
        created(): void {
            const today: Date = new Date();
            this.year = today.getFullYear();
        }
    });
</script>

<style scoped>
    footer {
        margin-top: -50px;
        height: 50px;
        display: flex;
        align-items: center;
        justify-content: center;
    }
</style>
