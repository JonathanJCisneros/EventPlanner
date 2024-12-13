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
            Navigation
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
                        if (data) {
                            this.loading = false;
                            this.content = data;
                        }
                        else {
                            attempts++;

                            if (attempts < 2) {
                                await this.getContent(attempts);
                            }                            
                        }
                    })
                    .catch(async (error: Error): void => {
                        attempts++;

                        if (attempts < 2) {
                            await this.getContent(attempts);
                        }  
                    });
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
