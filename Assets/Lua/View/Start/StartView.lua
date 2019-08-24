local len = string.len
local wait = coroutine.wait
local start = coroutine.start

local luaBehaviour;
local transform;
local gameObject;

StartView = BaseView:New('StartView');
local this = StartView;

--启动事件--
function StartView.Awake(obj)
	gameObject = obj;
	transform = obj.transform;
	luaBehaviour = transform:GetComponent(typeof(LuaBehaviour));

	this.BtnKaiche = transform:Find('kaicheBtn'):GetComponent(typeof(Button));
	
	luaBehaviour:AddClick(this.BtnKaiche, this.OnClick);
	logWarn("Start lua--->>"..gameObject.name);
end

function StartView.OnEnable()
	SoundManager.PlayBackSound(SoundName.BgmLogin);		--播放登录音乐
end

function StartView.OnDisable()
end

local Switch = 
{
	kaicheBtn = function()
		SoundManager.Play(SoundName.Start);		
		this.SendNotification(NotifyName.ShowUI, ChatMainView);				--显示登录
		this:Close()
	end,
}

--单击事件--
function StartView.OnClick(go)
	log("OnClick---->>>"..go.name);
	local func = Switch[go.name];
	if func then func() end;
end

--单击事件--
function StartView.OnDestroy()
	logWarn("OnDestroy---->>>");
end

--通知列表--
this.NotifyList = 
{
}

return this;