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
                        subheading="Row Layout"
                        details="
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt
                            ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco
                            laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate
                            velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt
                            in culpa qui officia deserunt mollit anim id est laborum."
                        :colors="{
                            font: 'white',
                            background: 'blue'
                        }" />
        <DetailsSection orientation="row-reverse"
                        :image="{
                            source: './src/assets/img/no-photo.png',
                            altText: 'No Photo'
                        }"
                        subheading="Row Reverse Layout"
                        details="
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt
                            ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco
                            laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate
                            velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt
                            in culpa qui officia deserunt mollit anim id est laborum."
                        :colors="{
                            font: 'white',
                            background: 'green'
                        }" />
        <DetailsSection orientation="column"
                        :image="{
                            source: './src/assets/img/no-photo.png',
                            altText: 'No Photo'
                        }"
                        subheading="Column Layout"
                        details="
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt
                            ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco
                            laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate
                            velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt
                            in culpa qui officia deserunt mollit anim id est laborum."
                        :colors="{
                            font: 'white',
                            background: 'purple'
                        }" />
        <DetailsSection orientation="column-reverse"
                        :image="{
                            source: './src/assets/img/no-photo.png',
                            altText: 'No Photo'
                        }"
                        subheading="Column Reverse Layout"
                        details="
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt
                            ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco
                            laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate
                            velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt
                            in culpa qui officia deserunt mollit anim id est laborum."
                        :colors="{
                            font: 'white',
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
    }

    interface Data {
        loading: boolean,
        content: ImageContent[]
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
        async created(): Promise<void> {
            await this.getContent();
        },
        methods: {
            async getContent(attempts: number = 0): Promise<void> {
                await fetch('https://localhost:7134/api/home/getcontent', {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    }
                })
                    .then(async (response: Response): Promise<null | ImageContent[]> => {
                        if (!response.ok) {
                            return null;
                        }

                        return await response.json() as ImageContent[];
                    })
                    .then(async (data: null | ImageContent[]): Promise<void> => {
                        if (!data) {
                            await this.handleFail(attempts);

                            return;
                        }

                        this.loading = false;
                        this.content = data as ImageContent[];
                    })
                    .catch(async (error: Error): Promise<void> => {
                        console.log(error);

                        await this.handleFail(attempts);
                    });
            },
            async handleFail(attempts: number): Promise<void> {
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
        margin-bottom: 15px;
    }

    img {
        margin-bottom: 15px;
    }
</style>
