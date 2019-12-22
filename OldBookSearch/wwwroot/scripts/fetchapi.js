window.fetchHttp = {
    getHtmlAsync: async function (url, isEncodeSJis) {
        const proxyUrl = "https://cors-anywhere.herokuapp.com/";
        let actUrl = proxyUrl + url;
        // console.log(actUrl);
        // console.log("isShiftJis", isEncodeSJis);
        const res = await fetch(actUrl, {
            method: "GET",
            mode: "cors",
            headers: {
                "Content-Type": "text/html",
            },
        });

        if (!isEncodeSJis) {
            const text = await res.text();
            // console.log(text);
            return text;
        } else {
            const blob = await res.blob()
            const fr = new FileReader()
            return await new Promise((resolve, reject) => {
                fr.onload = eve => {
                    // console.log(fr.result)
                    resolve(fr.result)
                }
                fr.onerror = err => reject(err)
                fr.readAsText(blob, "Shift_JIS")
            })

        }

    }
};