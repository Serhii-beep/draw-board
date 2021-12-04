<template>
    <aside class="sidebar">
        <div id="leftside-navigation" class="nano">
            <ul class="nano-content">
                <li>
                    <i class="fas fa-palette"></i>
                    <span id="colorInput"><input type="color" v-model="lineColor"></span>
                </li>
                <li>
                    <i class="fas fa-check-double"></i>
                    <span><input type="range" min="1" max="30" v-model="lineWidth"></span>
                </li>
                <li @click="clearCanvas" id="clear">
                    <i class="far fa-trash-alt"></i>
                    <span>Clear all</span>
                </li>
            </ul>
        </div>
    </aside>
    <canvas id="canvas"
        @mousedown="startDrawing"
        @mouseup="stopDrawing"
        @mouseout="stopDrawing"
        @mousemove="draw"
    ></canvas>
</template>

<script>
import {HubConnectionBuilder} from '@aspnet/signalr'
export default {
    data() {
        return {
            context: null,
            isDrawing: false,
            isClearing: false,
            lineColor: '',
            lineWidth: 5,
            connection: null
        }
    },

    methods: {
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
            this.isDrawing = false;
            this.isClearing = false;
            this.context.beginPath();
        },

        draw(e) {
            if(!this.isDrawing) return;
            const x = e.clientX;
            const y = e.clientY;
            this.drawCanvas(x, y);
            this.connection.invoke('Draw', x, y);
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
            this.context.clearRect(0, 0, canvas.width, canvas.height);
        }
    },

    mounted() {
        const canvas = document.querySelector("#canvas");
        this.context = canvas.getContext("2d");
        canvas.height = window.innerHeight;
        canvas.width = window.innerWidth;
        window.addEventListener("contextmenu", e => e.preventDefault());
        this.connection = new HubConnectionBuilder().withUrl("https://localhost:44348/draw").build();
        this.connection.start();
        this.connection.on('draw', (x, y) => this.drawCanvas(x, y));
        console.log(this.connection);
    },

    beforeUnmount() {
        connection.off('draw', this.drawCanvas);
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

.sidebar {
    width: 200px;
    height: 100%;
    background: #293949;
    position: absolute;
    -webkit-transition: all 0.3s ease-in-out;
    -moz-transition: all 0.3s ease-in-out;
    -o-transition: all 0.3s ease-in-out;
    -ms-transition: all 0.3s ease-in-out;
    transition: all 0.3s ease-in-out;
    z-index: 100;
}

.sidebar #leftside-navigation ul {
    margin: -2px 0 0;
    padding: 0;
}

.sidebar #leftside-navigation ul li {
    display: flex;
    justify-content: space-between;
    align-items: center;
    list-style-type: none;
    border-bottom: 1px solid rgba(255, 255, 255, 0.05);
    margin-top: 10px;
    margin-bottom: 10px;
    padding: 10px;
}
i {
    color: white;
    padding: 5px;
}

#colorInput {
    width: 129px;
}

#colorInput input {
    width: 100%;
}

#clear {
    cursor: pointer;
}

::selection {
    background: transparent;
}

::-webkit-scrollbar {
    display: none;
}

#canvas {
    overflow: scroll;
}
</style>