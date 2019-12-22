let dotNetObjRef;
function startCapture(dotNetObj) {
    dotNetObjRef = dotNetObj
    if (!dotNetObjRef) {
        console.error("dotNetObj ref is null");
        return;
    }
    // Quaggaの設定項目
    const config = {
        // カメラの映像の設定
        inputStream: {
            type: "LiveStream",
            // カメラ映像を表示するHTML要素の設定
            target: document.querySelector("#camera-area"),
            // バックカメラの利用を設定. (フロントカメラは"user")
            constraints: {
                facingMode: "environment"
            },
            size: 800,
            // 検出範囲の指定: 上下30%は対象外
            area: { top: "30%", right: "0%", left: "0%", bottom: "30%" }
        },
        // 解析するワーカ数の設定
        numOfWorkers: navigator.hardwareConcurrency || 4,
        // バーコードの種類を設定: ISBNは"ean_reader"
        decoder: { readers: ["ean_reader"] }
    }
    Quagga.onDetected(onDetected);
    Quagga.onProcessed(onProcessed);
    Quagga.init(config, onInitilize);
}
function onDetected(success) {
    // ISBNは'success.codeResult.code'から取得
    const isbn = success.codeResult.code;
    // console.log("isbn:", isbn);
    if (isbn.startsWith("978") && isbn.length === 13) {
        // console.log("match!!!!!");
        if (dotNetObjRef) {
            console.log("invokeMethod");
            dotNetObjRef.invokeMethod('CodeDetected', isbn);
            stopCapture();
        }
    } else {
        console.log("no isbn");
    }
}
function onInitilize(error) {
    if (!!error) {
        console.error(`Error: ${error}`, error);
        return;
    }
    // エラーがない場合は、読み取りを開始
    console.info("Initialization finished. Ready to start");
    switchDetctArea(true);
    Quagga.start();
}

function stopCapture() {
    if (!!Quagga) Quagga.stop();
    switchDetctArea(false);
}

function onProcessed(data) {
    const ctx = Quagga.canvas.ctx.overlay;
    const canvas = Quagga.canvas.dom.overlay;

    if (!data) return;

    // 認識したバーコードを緑の枠で囲む
    if (data.boxes) {
        ctx.clearRect(0, 0, canvas.width, canvas.height);

        const hasNotRead = box => box !== data.box;
        data.boxes.filter(hasNotRead).forEach(box => {
            Quagga.ImageDebug.drawPath(box, { x: 0, y: 1 }, ctx, { color: "green", lineWidth: 2 });
        });
        // 読み取ったバーコードを青の枠で囲む
        if (data.box) {
            Quagga.ImageDebug.drawPath(data.box, { x: 0, y: 1 }, ctx, { color: "blue", lineWidth: 2 });
        }
        // 読み取ったバーコードに赤い線を引く
        if (data.codeResult && data.codeResult.code) {
            Quagga.ImageDebug.drawPath(data.line, { x: "x", y: "y" }, ctx, { color: "red", lineWidth: 3 });
        }
    }
}

function switchDetctArea(isShow) {
    // switch display
    const detectArea = document.querySelector("#detect-area");
    if (isShow) {
        detectArea.style.visibility = "visible"
    } else {
        detectArea.style.visibility = "hidden"
    }

}