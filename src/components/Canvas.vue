<template>
    <canvas id="canvas"
        @mousedown="startDrawing"
        @mouseup="stopDrawing"
        @mouseout="stopDrawing"
        @mousemove="draw"
    ></canvas>
</template>
<script>
import {mapState, mapMutations} from 'vuex'
export default {
    methods: {
        ...mapMutations({
            setCanvas: 'canvas/setCanvas',
            setContext: 'canvas/setContext',
            setIsDrawing: 'canvas/setIsDrawing',
            setIsClearing: 'canvas/setIsClearing'
        })
    },

    computed: {
        ...mapState({
            context: state => state.canvas.context,
            canvas: state => state.canvas.canvas,
            isDrawing: state => state.canvas.isDrawing,
            isClearing: state => state.canvas.isClearing
        })
    }
}
</script>
<style scoped>
#canvas {
    overflow: scroll;
}
</style>