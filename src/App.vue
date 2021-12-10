<template>
    <side-bar @clearCanvas="clearCanvas"></side-bar>
    <my-canvas ref="canvas"></my-canvas>
</template>

<script>
import SideBar from '@/components/SideBar'
import MyCanvas from '@/components/MyCanvas.vue';
import { mapMutations } from 'vuex'
export default {
    components: {
        SideBar,
        MyCanvas
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
            setConnection: 'setConnection'
        }),

        clearCanvas() {
            this.$refs.canvas.clearCanvas();
        }
    },

    mounted() {
        window.addEventListener("contextmenu", e => e.preventDefault());
        this.setConnection();
        
        /*
        this.connection.on('draw', (x, y, color, width, globalCompositeOperation) => {
            this.setLineColor(color);
            this.setLineWidth(width);
            this.context.globalCompositeOperation = globalCompositeOperation;
            this.drawCanvas(x, y)
        });
        this.connection.on('clear', () => this.clear());
        this.connection.on('stopDrawing', () => this.stop());*/
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