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
    data() {
        return {
            context: null,
            canvas: null,
            isDrawing: false,
            isClearing: false
        }
    },

    methods: {
        ...mapMutations({
            setLineColor: 'drawOptions/setLineColor',
            setLineWidth: 'drawOptions/setLineWidth',
            setConnection: 'setConnection'
        }),

        startDrawing(e) {
            this.isDrawing = true;
            if(this.isLeftButton()) {
                this.context.globalCompositeOperation = "source-over";
            } else {
                this.context.globalCompositeOperation = "destination-out";
            }
            this.draw(e);
        },

        draw(e) {
            if(!this.isDrawing) return;
            const x = e.clientX;
            const y = e.clientY;
            this.drawCanvas(x, y);
            this.connection.invoke('Draw', x, y, this.lineColor, parseInt(this.lineWidth), this.context.globalCompositeOperation);
        },

        drawCanvas(x, y) {
            this.context.lineWidth = this.lineWidth;
            this.context.strokeStyle = this.lineColor;
            this.context.lineCap = "round";
            this.context.lineJoin = "round";
            this.context.lineTo(x, y);
            this.context.stroke();
            this.context.beginPath();
            this.context.moveTo(x, y);
        },

        stopDrawing() {
            this.stop();
            this.connection.invoke("StopDrawing");
        },

        stop() {
            this.isDrawing = false;
            this.isClearing = false;
            this.context.beginPath();
        },

        isLeftButton(e) {
            e = e || window.event;
            if ("buttons" in e) {
                return e.buttons == 1;
            }
            var button = e.which || e.button;
            return button == 1;
        },

        clearCanvas() {
            this.clear();
            this.connection.invoke("Clear");
        },

        clear() {
            this.context.clearRect(0, 0, this.canvas.width, this.canvas.height);
        }
    },

    mounted() {
        const canvas = document.querySelector("#canvas");
        this.context = canvas.getContext("2d");
        canvas.height = window.innerHeight;
        canvas.width = window.innerWidth;
        this.canvas = canvas;  
        if(!this.isConnected) {
            this.setConnection();
        }      
        this.connection.on('draw', (x, y, color, width, globalCompositeOperation) => {
            this.setLineColor(color);
            this.setLineWidth(width);
            this.context.globalCompositeOperation = globalCompositeOperation;
            this.drawCanvas(x, y)
        });
        this.connection.on('clear', () => this.clear());
        this.connection.on('stopDrawing', () => this.stop());
    },

        beforeUnmount() {
        this.connection.off('draw', this.drawCanvas);
        this.connection.off('clear', this.clear);
        this.connection.off('stopDrawing', this.stop);
    },

    computed: {
        ...mapState({
            connection: state => state.connection,
            isConnected: state => state.isConnected,
            lineColor: state => state.drawOptions.lineColor,
            lineWidth: state => state.drawOptions.lineWidth
        })
    }
}
</script>
<style scoped>
#canvas {
    overflow: scroll;
}
</style>