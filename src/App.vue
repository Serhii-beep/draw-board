<template>
    <side-bar @clearCanvas="clearCanvas"></side-bar>
    <canvas id="canvas"
        @mousedown="startDrawing"
        @mouseup="stopDrawing"
        @mouseout="stopDrawing"
        @mousemove="draw"
    ></canvas>
</template>

<script>
import {HubConnectionBuilder} from '@aspnet/signalr'
import SideBar from '@/components/SideBar.vue'
import {mapState, mapMutations} from 'vuex'
export default {
    components: {
        SideBar
    },

    data() {
        return {
            context: null,
            canvas: null,
            isDrawing: false,
            isClearing: false,
            connection: null
        }
    },

    methods: {
        ...mapMutations({
            setLineColor: 'drawOptions/setLineColor',
            setLineWidth: 'drawOptions/setLineWidth'
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

        stopDrawing() {
            this.stop();
            this.connection.invoke("StopDrawing");
        },

        stop() {
            this.isDrawing = false;
            this.isClearing = false;
            this.context.beginPath();
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

    computed: {
        ...mapState({
            lineColor: state => state.drawOptions.lineColor,
            lineWidth: state => state.drawOptions.lineWidth
        })
    },

    mounted() {
        const canvas = document.querySelector("#canvas");
        this.context = canvas.getContext("2d");
        canvas.height = window.innerHeight;
        canvas.width = window.innerWidth;
        this.canvas = canvas;
        window.addEventListener("contextmenu", e => e.preventDefault());
        this.connection = new HubConnectionBuilder().withUrl("https://localhost:44348/draw").build();
        this.connection.start();
        this.connection.on('draw', (x, y, color, width, globalCompositeOperation) => {
            this.setLineColor(color);
            this.setLineWidth(width);
            this.context.globalCompositeOperation = globalCompositeOperation;
            this.drawCanvas(x, y)
        });
        this.connection.on('clear', () => this.clear());
        this.connection.on('stopDrawing', () => this.stop());
        console.log(this.connection);
    },

    beforeUnmount() {
        this.connection.off('draw', this.drawCanvas);
        this.connection.off('clear', this.clear);
        this.connection.off('stopDrawing', this.stop);
    }
    
}
</script>

<style>
* {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
    overflow: hidden;
}

@import url('//netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.min.css');
@font-face {
  font-family: 'Open Sans';
  font-style: normal;
  font-weight: 300;
  font-stretch: normal;
  src: url(https://fonts.gstatic.com/s/opensans/v27/memSYaGs126MiZpBA-UvWbX2vVnXBbObj2OVZyOOSr4dVJWUgsiH0B4gaVc.ttf) format('truetype');
}
@font-face {
  font-family: 'Open Sans';
  font-style: normal;
  font-weight: 400;
  font-stretch: normal;
  src: url(https://fonts.gstatic.com/s/opensans/v27/memSYaGs126MiZpBA-UvWbX2vVnXBbObj2OVZyOOSr4dVJWUgsjZ0B4gaVc.ttf) format('truetype');
}
@font-face {
  font-family: 'Open Sans';
  font-style: normal;
  font-weight: 700;
  font-stretch: normal;
  src: url(https://fonts.gstatic.com/s/opensans/v27/memSYaGs126MiZpBA-UvWbX2vVnXBbObj2OVZyOOSr4dVJWUgsg-1x4gaVc.ttf) format('truetype');
}
body {
    color: #5D5F63;
    background: #ffffff;
    font-family: 'Open Sans', sans-serif;
    padding: 0;
    margin: 0;
    text-rendering: optimizeLegibility;
    -webkit-font-smoothing: antialiased;
}

::selection {
    background: transparent;
}

::-webkit-scrollbar {
    display: none;
}
</style>