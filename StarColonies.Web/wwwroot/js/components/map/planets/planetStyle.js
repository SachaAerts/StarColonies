export function getStyle(image) {
    return `
        <style>
            .planet-container {
                position: absolute;
                width: 100px;
                height: 100px;
                background-size: cover;
                background-repeat: no-repeat;
                background-position: center;
                background-image: url("${image}");
                border-radius: 50%;
                cursor: pointer;
                z-index: 5000;
                transition: .5s ease-out;
            }

            .planet-container:active {
                transform: scale(1.2);
            }

            .selector-svg {
                position: absolute;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%) scale(1.1);
                animation: shrink-to-fit 0.4s ease-out forwards;
                pointer-events: none;
            }

            @keyframes shrink-to-fit {
                from {
                    transform: translate(-50%, -50%) scale(1.2);
                    opacity: 0.2;
                }
                to {
                    transform: translate(-50%, -50%) scale(1);
                    opacity: 1;
                }
            }

            .quest-panel {
                position: absolute;
                padding : 10px 5px;
                
                width: 244px; height: 330px;
                
                background-color: rgba(217, 217, 217, 0.06);
                
                border: 2px solid #152F49;
                border-radius: 6px;
                
                z-index: 9999;

                transform-origin: center center;
                transform: scaleY(0);
                animation: panel-grow 0.3s ease-out forwards;
            }

            @keyframes panel-grow {
                0% {
                    transform: scaleY(0);
                    opacity: 0;
                }
                100% {
                    transform: scaleY(1);
                    opacity: 1;
                }
            }
            
            @keyframes panel-shrink {
                0% {
                    transform: scaleY(1);
                    opacity: 1;
                }
                20% {
                    transform: scaleY(0.8);
                    opacity: 1;
                }
                40% {
                    transform: scaleY(0.6);
                    opacity: 1;
                }
                60% {
                    transform: scaleY(0.4);
                    opacity: 1;
                }
                80% {
                    transform: scaleY(0.2);
                    opacity: 1;
                }
                90% {
                    transform: scaleY(0.1);
                    opacity: 1;
                }
                100% {
                    transform: scaleY(0);
                    opacity: 0;
                }
            }  

            .quest-scroll::-webkit-scrollbar {
                width: 6px;
            }

            .quest-scroll::-webkit-scrollbar-track {
                background: transparent;
            }

            .quest-scroll::-webkit-scrollbar-thumb {
                background-color: #152F49;
                border-radius: 10px;
            }

            .quest-scroll.none {
                display:none;
            }
            .quest-scroll {
                position: relative;
                height: 100%; width: 100%;
                overflow-y: auto;
                scrollbar-width: thin;
                scrollbar-color: #152F49 transparent;
                display: flex;
                flex-direction: column;
                gap: 15px;
            }
            
            .quest-frame {
                color: rgba(255,255,255,0.66);
                
                font-family: 'Judge', sans-serif;
                
                background: #152F49;
                
                border: 2px solid rgba(36,77,117,0.63);
                border-radius: 6px;
                
                padding: 10px;
                
                transition: all .2s ease-out;
            }
            
            .quest-frame:hover {
                transform: scale(0.95);
            }
            
            .quest-frame h3 {
                font-family: 'Judge Italic', sans-serif;
                padding: 0; margin: 0; 
            }
            
            small {
                display: flex;
                align-items: center;
                justify-content: end;
            }
            .quest-panel p {
                margin: 0; padding: 0;
            }
        </style>
    `;
}
