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
                    <span><input type="range" min="1" max="15" v-model="lineWidth"></span>
                </li>
                <li @click="clearCanvas" id="clear">
                    <i class="fas fa-eraser"></i>
                    <span>Clear</span>
                </li>
            </ul>
        </div>
    </aside>
    <canvas id="canvas"
        @mousedown="startDrawing"
        @mouseup="stopDrawing"
        @mousemove="draw"
    ></canvas>
</template>

<script>
export default {
    data() {
        return {
            context: null,
            isDrawing: false,
            lineColor: '',
            lineWidth: 5
        }
    },

    methods: {
        startDrawing(e) {
            this.isDrawing = true;
            this.draw(e);
        },

        stopDrawing() {
            this.isDrawing = false;
            this.context.beginPath();
        },

        draw(e) {
            if(!this.isDrawing) return;
            this.context.lineWidth = this.lineWidth;
            this.context.strokeStyle = this.lineColor;
            this.context.lineCap = "round";
            this.context.lineTo(e.clientX, e.clientY);
            this.context.stroke();
            this.context.beginPath();
            this.context.moveTo(e.clientX, e.clientY);
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
    }
    
}
</script>

<style>
* {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
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
</style>