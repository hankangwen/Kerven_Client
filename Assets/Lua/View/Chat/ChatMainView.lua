local len = string.len
local wait = coroutine.wait
local start = coroutine.start

local luaBehaviour;
local transform;
local gameObject;
local netWork;
local msgText;
local clientName;
local msgShowText;
local UpdateBeat = UpdateBeat

ChatMainView = BaseView:New('ChatMainView');
local this = ChatMainView;

--启动事件--
function ChatMainView.Awake(obj)
	gameObject = obj;
	transform = obj.transform;
	luaBehaviour = transform:GetComponent(typeof(LuaBehaviour));
	
	netWork = NetWorkManagerTag:GetComponent(typeof(NetWorkManager));
	msgText = transform:Find('InputMsg/Text'):GetComponent(typeof(Text));
	clientName = transform:Find('InputName/Text'):GetComponent(typeof(Text));
	msgShowText = transform:Find('MsgBg/Msg'):GetComponent(typeof(Text));
	this.SendBtn = transform:Find('SendBtn'):GetComponent(typeof(Button));
	
	luaBehaviour:AddClick(this.SendBtn, this.OnClick);

	if not this.handle then
		this.handle = UpdateBeat:CreateListener(this.Update)
	end
	UpdateBeat:AddListener(this.handle)	

	logWarn("Start lua--->>"..gameObject.name);
end

function ChatMainView.Update()
	if netWork:CheckHasMsg() then
		print("CheckHasMsg()", netWork:CheckHasMsg())
		msgShowText.text = string.format("%s\n%s", msgShowText.text, netWork:GetAndRemoveMsg())
	end	
end

function ChatMainView.OnEnable()
	SoundManager.PlayBackSound(SoundName.BgmLogin);		--播放登录音乐
end

function ChatMainView.OnDisable()
end

local Switch = 
{
	SendBtn = function()
        netWork:SendMsg(msgText.text, clientName.text, 0)
	end,
}

--单击事件--
function ChatMainView.OnClick(go)
	log("OnClick---->>>"..go.name);
	local func = Switch[go.name];
	if func then func() end;
end

--单击事件--
function ChatMainView.OnDestroy()
	logWarn("OnDestroy---->>>");
	if this.handle then
		UpdateBeat:RemoveListener(this.handle)	
	end
end

--通知列表--
this.NotifyList = 
{
}

return this;