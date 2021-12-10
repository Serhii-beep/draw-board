<template>
    <aside class="sidebar">
        <div id="leftside-navigation" class="nano">
            <ul class="nano-content">
                <li>
                    <i class="fas fa-palette"></i>
                    <span id="colorInput"><input
                    type="color" 
                    :value="lineColor" 
                    @input="setColor"
                    ></span>
                </li>
                <li>
                    <i class="fas fa-check-double"></i>
                    <span><input 
                    type="range" 
                    min="1" 
                    max="30"
                    :value="lineWidth" 
                    @input="setWidth"
                    ></span>
                </li>
                <li @click="$emit('clearCanvas')" id="clear">
                    <i class="far fa-trash-alt"></i>
                    <span>Clear all</span>
                </li>
            </ul>
        </div>
    </aside>
</template>
<script>
import {mapState, mapMutations} from 'vuex'
export default {
    emits: [
        "clearCanvas"
    ],

    methods: {
        ...mapMutations({
            setLineColor: 'drawOptions/setLineColor',
            setLineWidth: 'drawOptions/setLineWidth'
        }),
        setWidth(e) {
            this.setLineWidth(e.target.value);
        },
        setColor(e) {
            this.setLineColor(e.target.value);
        }
    },
    computed: {
        ...mapState({
            lineColor: state => state.drawOptions.lineColor,
            lineWidth: state => state.drawOptions.lineWidth
        })
    }
}
</script>
<style scoped>
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