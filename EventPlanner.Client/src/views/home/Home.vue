<template>
    <Title title="Welcome to the Event Planner!" />
    <div class="container">
        <div class="loading skeleton" v-if="loading"></div>
        <Carousel :items-to-show="1" v-else>
            <Slide v-for="image in content" :key="image.id">
                <img :src="image.source" :alt="image.altText" />
            </Slide>
            <template #addons>
                <Navigation />
                <Pagination />
            </template>
        </Carousel>
        <DetailsSection orientation="row"
                        :image="{
                            source: './src/assets/img/no-photo.png',
                            altText: 'No Photo'
                        }"
                        details="Row Layout"
                        :colors="{
                            font: 'black',
                            background: 'blue'
                        }" />
        <DetailsSection orientation="row-reverse"
                        :image="{
                            source: './src/assets/img/no-photo.png',
                            altText: 'No Photo'
                        }"
                        details="Reverse Row Layout"
                        :colors="{
                            font: 'white',
                            background: 'green'
                        }" />
        <DetailsSection orientation="column"
                        :image="{
                            source: './src/assets/img/no-photo.png',
                            altText: 'No Photo'
                        }"
                        details="Column Layout"
                        :colors="{
                            font: 'white',
                            background: 'purple'
                        }" />
        <DetailsSection orientation="column-reverse"
                        :image="{
                            source: './src/assets/img/no-photo.png',
                            altText: 'No Photo'
                        }"
                        details="Reverse Column Layout"
                        :colors="{
                            font: 'black',
                            background: 'red'
                        }" />
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import 'vue3-carousel/dist/carousel.css';
    import { Carousel, Slide, Pagination, Navigation } from 'vue3-carousel';
    import Title from '../../components/Title.vue';
    import DetailsSection from '../../components/DetailsSection.vue';

    type ImageContent = {
        id: string,
        source: string,
        altText: string,
        createdDate: Date,
        updatedDare: Date
    }[]

    interface Data {
        loading: boolean,
        content: ImageContent
    }

    export default defineComponent({
        data(): Data {
            return {
                loading: true,
                content: []
            };
        },
        components: {
            Title,
            Carousel,
            Slide,
            Pagination,
            Navigation,
            DetailsSection
        },
        async created(): void {
            await this.getContent();
        },
        methods: {
            async getContent(attempts: number = 0): void {
                await fetch('https://localhost:7134/api/home/getcontent', {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json'
                    }
                })
                    .then(async (response: Response): null | ImageContent => {
                        if (!response.ok) {
                            return null;
                        }

                        return await response.json();
                    })
                    .then(async (data: null | ImageContent): void => {
                        if (!data) {
                            await this.handleFail(attempts);

                            return;
                        }

                        this.loading = false;
                        this.content = data;
                    })
                    .catch(async (error: Error): void => {
                        await this.handleFail(attempts);
                    });
            },
            async handleFail(attempts: number): void {
                attempts++;

                if (attempts < 2) {
                    await this.getContent(attempts);
                }  
            }
        }
    });
</script>

<style scoped>
    .container {
        display: flex;
        flex-direction: column;
        align-items: center;
        padding: 15px;
    }

    .loading {
        max-width: 650px;
        width: 100%;
        aspect-ratio: 16/9;
    }

    img {
        margin-bottom: 15px;
    }
</style>
