import React,{ useEffect, useState } from 'react'

function ProfileWindow() {
    const [image,setImage] = useState<any>("https://thumbs.dreamstime.com/b/default-avatar-profile-image-vector-social-media-user-icon-potrait-182347582.jpg");
    
    const onSelectFile = (event) => {
        console.log('smthg');
        if (event.target.files && event.target.files[0]) {
          var reader = new FileReader();
        
          reader.readAsDataURL(event.target.files[0]);

          reader.onload = (event) => { 
              if(event.target != null){
                setImage(event.target.result)
              }
          }

        }
    }

    return (
        <div>
            <img className="profile-image" src={image} alt=""/>
            <input type="file" accept="image/*" onChange={onSelectFile}></input>
            <button>Change profile image</button>
            <button>Change password</button>
            <button>Change email</button>
            <button>Change username</button>
        </div>
    )
}

export default ProfileWindow
