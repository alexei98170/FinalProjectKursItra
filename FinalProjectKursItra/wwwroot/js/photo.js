document.getElementById("upload_widget_opener").addEventListener("click", function () {
    cloudinary.openUploadWidget({
        cloud_name: 'mycloud6855', upload_preset: 'kq1ziypj',
        sources: ['local', 'url', 'camera'], cropping: 'server',
        cropping_aspect_ratio: '1', theme: 'white'
    },
        function (error, result) {
            console.log(error, result)
            img = result[0].public_id
            version = result[0].version
            var theElement = document.getElementById("photo")
            theElement.src = "http://res.cloudinary.com/mycloud6855/image/upload/v" + version + "/" + img + ".png"
            document.getElementById("inPhoto").value = "http://res.cloudinary.com/mycloud6855/image/upload/v" + version + "/" + img + ".png"
        });
},
    false
);