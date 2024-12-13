<template>
    <section :class="orientation">
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

    export default defineComponent({
        props: {
            orientation: {
                type: String,
                required: true
            },
            image: {
                type: Object as PropType<ImageContent>,
                required: true
            }
            details: {
                type: String,
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
        padding: 1.25rem;
    }
</style>
