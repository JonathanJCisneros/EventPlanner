<template>
    <section :class="[orientation, classes]"
             :style="{
                'background-color': colors.background,
                'color': colors.font
             }">
        <div class="image-container">
            <img :src="image.source" :alt="image.altText" />
        </div>
        <div class="description" v-html="details"></div>
    </section>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import type { PropType } from 'vue';

    type ImageContent = {
        source: string,
        altText: string
    }

    type Colors = {
        font: string,
        background: string
    }

    export default defineComponent({
        props: {
            orientation: {
                type: String,
                required: true
            },
            classes: {
                type: String,
                required: false
            },
            image: {
                type: Object as PropType<ImageContent>,
                required: true
            },
            details: {
                type: String,
                required: true
            },
            colors: {
                type: Object as PropType<Colors>,
                required: true
            }
        }
    });
</script>

<style scoped>
    section {
        display: flex;
        align-items: center;
    }

        section.row {
            flex-direction: row;
        }

        section.row-reverse {
            flex-direction: row-reverse;
        }

        section.column {
            flex-direction: column;
        }

        section.column-reverse {
            flex-direction: column-reverse;
        }

            section.column-reverse .description, section.column .description {
                padding: 1.25rem;
            }

    section > div {
        flex: 1;        
    }

    .image-container {
        overflow: hidden;
        display: flex;
        align-items: center;
        justify-content: center;
    }

    img {
        width: 100%;
        height: auto;
    }

    .description {        
        text-align: center;
    }
</style>
