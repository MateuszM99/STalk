import React,{useState,useEffect } from 'react'
import './style.scss'
import PeopleIcon from '@material-ui/icons/People';
import PersonIcon from '@material-ui/icons/Person';
import ChatBubbleIcon from '@material-ui/icons/ChatBubble';
import PowerSettingsNewIcon from '@material-ui/icons/PowerSettingsNew';
import Badge from '@material-ui/core/Badge';
import { Link,useHistory } from 'react-router-dom';
import {getUsersFriendsRequestsRequest} from '../../services/api/ContactsRequests'
import {getProfileRequest} from '../../services/api/ProfileRequests'
import Condition from 'yup/lib/Condition';
interface Props {
    connected: boolean;
}
export const ChatNavbar: React.FC<Props> = ({ connected }) => {

    const history = useHistory();
    const [friendsRequests,setFriendsRequests] = useState(null);
    const [user,setUser] = useState(null);
    const [chatId,setChatId] = useState("show");

    const signOut = () => {
        localStorage.setItem("userData",'')
        localStorage.clear();
        history.push("/signIn");
    } 

    const getUsersFriendsRequests = async() => {
        try{
            let response = await getUsersFriendsRequestsRequest();
            console.log('requests' + response.data.addToContactRequests);
            setFriendsRequests(response.data.addToContactRequests); 
        } catch(err) {
        }
    }

    useEffect(() => {
        async function getData(){
            try{
                let response = await getProfileRequest();                              
                setUser(response.data); 
                // set latest chatId
            } catch(err) {
            }
            getUsersFriendsRequests();
        }
        
        if(user == null || friendsRequests == null){
        console.log(user);
        getData();
        }

      },[user,friendsRequests]);

    return (
        <div className="col-md-2 border-right" style={{background: '#285d80'}}>
            <div className="nav__image__display">               
                    <img src={`data:image/jpeg;base64,${user?.profileImage}`} alt=""/>
                    <div className="nav__image__display__text">
                        <h6>{user?.username}</h6>    
                        <p></p>                   
                    </div>
            </div>
            <div className="nav__tabs__display">
                <Link to={`/sTalk/chat/${chatId}`}>
                    <div>
                        <ChatBubbleIcon style={{color : 'white'}}/>
                        <p>Conversations</p>
                    </div>
                </Link>
                <Link to={`/sTalk/friends`}>
                <div>
                    <Badge badgeContent={friendsRequests == null ? 0 : friendsRequests.length} color="error">
                        <PeopleIcon style={{color : 'white'}}/>
                    </Badge>
                    <p>Friends</p>
                </div>
                </Link>
                <Link to={`/sTalk/profile`}>
                <div>
                    <PersonIcon style={{color : 'white'}}/>
                    <p>Profile</p>
                </div>
                </Link>
                <div className="offset" onClick={signOut}>
                    <PowerSettingsNewIcon/>
                    <p>Sign out</p>
                </div>
            </div>
            <div className="absolute-bottom">
                <p className="connection-text">Connection established: {connected ? "true" : "false"}</p>
            </div>
        </div>
    )
}

export default ChatNavbar
