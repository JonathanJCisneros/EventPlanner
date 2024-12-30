<template>
    <div class="site-wrapper">        
        <Header v-if="layout === undefined || layout.header === 'standard'" />
        <MinimalHeader v-else-if="layout.header === 'minimal'" />
        <main>
            <RouterView></RouterView>
        </main>
    </div>    
    <Footer v-if="layout === undefined || layout.footer === 'standard'" />
</template>

<script lang="ts">
    import { defineComponent, defineAsyncComponent } from 'vue';
    import { RouterView } from 'vue-router';
    import router from './routes/routes.ts';

    type Layout = {
        header: string,
        footer: string
    }

    export default defineComponent({
        components: {
            Header: defineAsyncComponent(() => import('./components/layouts/Header.vue')),
            MinimalHeader: defineAsyncComponent(() => import('./components/layouts/MinimalHeader.vue')),
            Footer: defineAsyncComponent(() => import('./components/layouts/Footer.vue'))
        },
        computed: {
            layout: (): Layout => router.currentRoute.value.meta.layout
        }      
    });
</script>
