    // The first step to creating a binding is to add your native framework ("MyLibrary.xcframework")
    // to the project.
    // Open your binding csproj and add a section like this
    // <ItemGroup>
    //   <NativeReference Include="MyLibrary.xcframework">
    //     <Kind>Framework</Kind>
    //     <Frameworks></Frameworks>
    //   </NativeReference>
    // </ItemGroup>
    //
    // Once you've added it, you will need to customize it for your specific library:
    //  - Change the Include to the correct path/name of your library
    //  - Change Kind to Static (.a) or Framework (.framework/.xcframework) based upon the library kind and extension.
    //    - Dynamic (.dylib) is a third option but rarely if ever valid, and only on macOS and Mac Catalyst
    //  - If your library depends on other frameworks, add them inside <Frameworks></Frameworks>
    // Example:
    // <NativeReference Include="libs\MyTestFramework.xcframework">
    //   <Kind>Framework</Kind>
    //   <Frameworks>CoreLocation ModelIO</Frameworks>
    // </NativeReference>
    // 
    // Once you've done that, you're ready to move on to binding the API...
    //
    // Here is where you'd define your API definition for the native Objective-C library.
    //
    // For example, to bind the following Objective-C class:
    //
    //     @interface Widget : NSObject {
    //     }
    //
    // The C# binding would look like this:
    //
    //     [BaseType (typeof (NSObject))]
    //     interface Widget {
    //     }
    //
    // To bind Objective-C properties, such as:
    //
    //     @property (nonatomic, readwrite, assign) CGPoint center;
    //
    // You would add a property definition in the C# interface like so:
    //
    //     [Export ("center")]
    //     CGPoint Center { get; set; }
    //
    // To bind an Objective-C method, such as:
    //
    //     -(void) doSomething:(NSObject *)object atIndex:(NSInteger)index;
    //
    // You would add a method definition to the C# interface like so:
    //
    //     [Export ("doSomething:atIndex:")]
    //     void DoSomething (NSObject object, nint index);
    //
    // Objective-C "constructors" such as:
    //
    //     -(id)initWithElmo:(ElmoMuppet *)elmo;
    //
    // Can be bound as:
    //
    //     [Export ("initWithElmo:")]
    //     NativeHandle Constructor (ElmoMuppet elmo);
    //
    // For more information, see https://aka.ms/ios-binding
    //

using System;
using CoreGraphics;
using CoreVideo;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Zoomios
{
    // @interface MobileRTCAuthService : NSObject
    [BaseType(typeof(NSObject))]
    interface MobileRTCAuthService
    {
        [Wrap("WeakDelegate")]
        [NullAllowed]
        MobileRTCAuthDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<MobileRTCAuthDelegate> _Nullable delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        // @property (retain, nonatomic) NSString * _Nullable jwtToken;
        [NullAllowed, Export("jwtToken", ArgumentSemantic.Retain)]
        string JwtToken { get; set; }

        // -(void)sdkAuth;
        [Export("sdkAuth")]
        void SdkAuth();

        // -(BOOL)isLoggedIn;
        [Export("isLoggedIn")]
        bool IsLoggedIn { get; }

        // -(MobileRTCUserType)getUserType;
        [Export("getUserType")]
        MobileRTCUserType UserType { get; }

        // -(NSString * _Nullable)generateSSOLoginWebURL:(NSString * _Nonnull)vanityUrl;
        [Export("generateSSOLoginWebURL:")]
        [return: NullAllowed]
        string GenerateSSOLoginWebURL(string vanityUrl);

        // -(MobileRTCLoginFailReason)ssoLoginWithWebUriProtocol:(NSString * _Nonnull)uriProtocol;
        [Export("ssoLoginWithWebUriProtocol:")]
        MobileRTCLoginFailReason SsoLoginWithWebUriProtocol(string uriProtocol);

        // -(BOOL)logoutRTC;
        [Export("logoutRTC")]
        bool LogoutRTC { get; }

        // -(MobileRTCAccountInfo * _Nullable)getAccountInfo;
        [NullAllowed, Export("getAccountInfo")]
        MobileRTCAccountInfo AccountInfo { get; }

        // -(void)enableAutoRegisterNotificationServiceForLogin:(BOOL)enable;
        [Export("enableAutoRegisterNotificationServiceForLogin:")]
        void EnableAutoRegisterNotificationServiceForLogin(bool enable);

        // -(MobileRTCSDKError)registerNotificationService:(NSString * _Nullable)accessToken;
        [Export("registerNotificationService:")]
        MobileRTCSDKError RegisterNotificationService([NullAllowed] string accessToken);

        // -(MobileRTCSDKError)unregisterNotificationService;
        [Export("unregisterNotificationService")]
        MobileRTCSDKError UnregisterNotificationService { get; }

        // -(MobileRTCNotificationServiceHelper * _Nullable)getNotificationServiceHelper;
        [NullAllowed, Export("getNotificationServiceHelper")]
        MobileRTCNotificationServiceHelper NotificationServiceHelper { get; }
    }

	// @protocol MobileRTCAuthDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface MobileRTCAuthDelegate
	{
		// @required -(void)onMobileRTCAuthReturn:(MobileRTCAuthError)returnValue;
		[Abstract]
		[Export ("onMobileRTCAuthReturn:")]
		void OnMobileRTCAuthReturn (MobileRTCAuthError returnValue);

		// @optional -(void)onMobileRTCAuthExpired;
		[Export ("onMobileRTCAuthExpired")]
		void OnMobileRTCAuthExpired ();

		// @optional -(void)onMobileRTCLoginResult:(MobileRTCLoginFailReason)resultValue;
		[Export ("onMobileRTCLoginResult:")]
		void OnMobileRTCLoginResult (MobileRTCLoginFailReason resultValue);

		// @optional -(void)onMobileRTCLogoutReturn:(NSInteger)returnValue;
		[Export ("onMobileRTCLogoutReturn:")]
		void OnMobileRTCLogoutReturn (nint returnValue);

		// @optional -(void)onNotificationServiceStatus:(MobileRTCNotificationServiceStatus)status;
		[Export ("onNotificationServiceStatus:")]
		void OnNotificationServiceStatus (MobileRTCNotificationServiceStatus status);
	}

	// @interface MobileRTCAccountInfo : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCAccountInfo
	{
		// -(NSString * _Nullable)getEmailAddress;
		[NullAllowed, Export ("getEmailAddress")]
		string EmailAddress { get; }

		// -(NSString * _Nullable)getUserName;
		[NullAllowed, Export ("getUserName")]
		string UserName { get; }

		// -(NSString * _Nullable)getPMIVanityURL;
		[NullAllowed, Export ("getPMIVanityURL")]
		string PMIVanityURL { get; }

		// -(BOOL)isTelephoneOnlySupported;
		[Export ("isTelephoneOnlySupported")]
		bool IsTelephoneOnlySupported { get; }

		// -(BOOL)isTelephoneAndVoipSupported;
		[Export ("isTelephoneAndVoipSupported")]
		bool IsTelephoneAndVoipSupported { get; }

		// -(BOOL)is3rdPartyAudioSupported;
		[Export ("is3rdPartyAudioSupported")]
		bool Is3rdPartyAudioSupported { get; }

		// -(NSString * _Nullable)get3rdPartyAudioInfo;
		[NullAllowed, Export ("get3rdPartyAudioInfo")]
		string Get3rdPartyAudioInfo { get; }

		// -(MobileRTCMeetingItemAudioType)getDefaultAudioInfo;
		[Export ("getDefaultAudioInfo")]
		MobileRTCMeetingItemAudioType DefaultAudioInfo { get; }

		// -(BOOL)onlyAllowSignedInUserJoinMeeting;
		[Export ("onlyAllowSignedInUserJoinMeeting")]
		bool OnlyAllowSignedInUserJoinMeeting { get; }

		// -(NSArray<MobileRTCAlternativeHost *> * _Nullable)getCanScheduleForUsersList;
		[NullAllowed, Export ("getCanScheduleForUsersList")]
		MobileRTCAlternativeHost[] CanScheduleForUsersList { get; }

		// -(BOOL)isLocalRecordingSupported;
		[Export ("isLocalRecordingSupported")]
		bool IsLocalRecordingSupported { get; }

		// -(BOOL)isCloudRecordingSupported;
		[Export ("isCloudRecordingSupported")]
		bool IsCloudRecordingSupported { get; }

		// -(MobileRTCMeetingItemRecordType)getDefaultAutoRecordType;
		[Export ("getDefaultAutoRecordType")]
		MobileRTCMeetingItemRecordType DefaultAutoRecordType { get; }

		// -(BOOL)isSpecifiedDomainCanJoinFeatureOn;
		[Export ("isSpecifiedDomainCanJoinFeatureOn")]
		bool IsSpecifiedDomainCanJoinFeatureOn { get; }

		// -(NSArray<NSString *> * _Nullable)getDefaultCanJoinUserSpecifiedDomains;
		[NullAllowed, Export ("getDefaultCanJoinUserSpecifiedDomains")]
		string[] DefaultCanJoinUserSpecifiedDomains { get; }
	}

	// @interface MobileRTCAlternativeHost : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCAlternativeHost
	{
		// @property (readonly, retain, nonatomic) NSString * _Nullable email;
		[NullAllowed, Export ("email", ArgumentSemantic.Retain)]
		string Email { get; }

		// @property (readonly, retain, nonatomic) NSString * _Nullable firstName;
		[NullAllowed, Export ("firstName", ArgumentSemantic.Retain)]
		string FirstName { get; }

		// @property (readonly, retain, nonatomic) NSString * _Nullable lastName;
		[NullAllowed, Export ("lastName", ArgumentSemantic.Retain)]
		string LastName { get; }

		// @property (readonly, assign, nonatomic) unsigned long long PMINumber;
		[Export ("PMINumber")]
		ulong PMINumber { get; }

		// -(id _Nonnull)initWithEmailAddress:(NSString * _Nonnull)emailAddress firstname:(NSString * _Nonnull)firstName lastName:(NSString * _Nonnull)lastName PMI:(unsigned long long)PMINumber;
		[Export ("initWithEmailAddress:firstname:lastName:PMI:")]
        System.IntPtr Constructor (string emailAddress, string firstName, string lastName, ulong PMINumber);
	}

	// @interface MobileRTCVideoRawData : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCVideoRawData
	{
		// @property (assign, nonatomic) char * yBuffer;
		[Export ("yBuffer", ArgumentSemantic.Assign)]
		unsafe IntPtr YBuffer { get; set; }

		// @property (assign, nonatomic) char * uBuffer;
		[Export ("uBuffer", ArgumentSemantic.Assign)]
		unsafe IntPtr UBuffer { get; set; }

		// @property (assign, nonatomic) char * vBuffer;
		[Export ("vBuffer", ArgumentSemantic.Assign)]
		unsafe IntPtr VBuffer { get; set; }

		// @property (assign, nonatomic) CGSize size;
		[Export ("size", ArgumentSemantic.Assign)]
		CGSize Size { get; set; }

		// @property (assign, nonatomic) MobileRTCFrameDataFormat format;
		[Export ("format", ArgumentSemantic.Assign)]
		MobileRTCFrameDataFormat Format { get; set; }

		// @property (assign, nonatomic) MobileRTCVideoRawDataRotation rotation;
		[Export ("rotation", ArgumentSemantic.Assign)]
		MobileRTCVideoRawDataRotation Rotation { get; set; }

		// -(BOOL)canAddRef;
		[Export ("canAddRef")]
		bool CanAddRef { get; }

		// -(BOOL)addRef;
		[Export ("addRef")]
		bool AddRef { get; }

		// -(NSInteger)releaseRef;
		[Export ("releaseRef")]
		nint ReleaseRef { get; }
	}

	// @interface MobileRTCAudioRawData : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCAudioRawData
	{
		// @property (assign, nonatomic) char * buffer;
		[Export ("buffer", ArgumentSemantic.Assign)]
		unsafe IntPtr Buffer { get; set; }

		// @property (assign, nonatomic) NSInteger bufferLen;
		[Export ("bufferLen")]
		nint BufferLen { get; set; }

		// @property (assign, nonatomic) NSInteger sampleRate;
		[Export ("sampleRate")]
		nint SampleRate { get; set; }

		// @property (assign, nonatomic) NSInteger channelNum;
		[Export ("channelNum")]
		nint ChannelNum { get; set; }

		// -(BOOL)canAddRef;
		[Export ("canAddRef")]
		bool CanAddRef { get; }

		// -(BOOL)addRef;
		[Export ("addRef")]
		bool AddRef { get; }

		// -(NSInteger)releaseRef;
		[Export ("releaseRef")]
		nint ReleaseRef { get; }
	}

	// @interface MobileRTCBOUser : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCBOUser
	{
		// -(NSString * _Nullable)getUserId;
		[NullAllowed, Export ("getUserId")]
		string UserId { get; }

		// -(NSString * _Nullable)getUserName;
		[NullAllowed, Export ("getUserName")]
		string UserName { get; }

		// -(MobileRTCBOUserStatus)getUserStatus;
		[Export ("getUserStatus")]
		MobileRTCBOUserStatus UserStatus { get; }
	}

	// @interface MobileRTCBOMeeting : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCBOMeeting
	{
		// -(NSString * _Nullable)getBOMeetingId;
		[NullAllowed, Export ("getBOMeetingId")]
		string BOMeetingId { get; }

		// -(NSString * _Nullable)getBOMeetingName;
		[NullAllowed, Export ("getBOMeetingName")]
		string BOMeetingName { get; }

		// -(NSArray<NSString *> * _Nullable)getBOMeetingUserList;
		[NullAllowed, Export ("getBOMeetingUserList")]
		string[] BOMeetingUserList { get; }
	}

	// @interface MobileRTCBOOption : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCBOOption
	{
		// @property (assign, nonatomic) MobileRTCBOStopCountDown countdownSeconds;
		[Export ("countdownSeconds", ArgumentSemantic.Assign)]
		MobileRTCBOStopCountDown CountdownSeconds { get; set; }

		// @property (assign, nonatomic) BOOL isParticipantCanChooseBO;
		[Export ("isParticipantCanChooseBO")]
		bool IsParticipantCanChooseBO { get; set; }

		// @property (assign, nonatomic) BOOL isParticipantCanReturnToMainSessionAtAnyTime;
		[Export ("isParticipantCanReturnToMainSessionAtAnyTime")]
		bool IsParticipantCanReturnToMainSessionAtAnyTime { get; set; }

		// @property (assign, nonatomic) BOOL isAutoMoveAllAssignedParticipantsEnabled;
		[Export ("isAutoMoveAllAssignedParticipantsEnabled")]
		bool IsAutoMoveAllAssignedParticipantsEnabled { get; set; }

		// @property (assign, nonatomic) BOOL isBOTimerEnabled;
		[Export ("isBOTimerEnabled")]
		bool IsBOTimerEnabled { get; set; }

		// @property (assign, nonatomic) BOOL isTimerAutoStopBOEnabled;
		[Export ("isTimerAutoStopBOEnabled")]
		bool IsTimerAutoStopBOEnabled { get; set; }

		// @property (assign, nonatomic) NSInteger timerDuration;
		[Export ("timerDuration")]
		nint TimerDuration { get; set; }
	}

	// @interface MobileRTCBOCreator : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCBOCreator
	{
		// -(NSString * _Nullable)createBO:(NSString * _Nonnull)boName;
		[Export ("createBO:")]
		[return: NullAllowed]
		string CreateBO (string boName);

		// -(BOOL)createGroupBO:(NSArray<NSString *> * _Nonnull)boNameList;
		[Export ("createGroupBO:")]
		bool CreateGroupBO (string[] boNameList);

		// -(BOOL)updateBO:(NSString * _Nonnull)boId name:(NSString * _Nonnull)boName;
		[Export ("updateBO:name:")]
		bool UpdateBO (string boId, string boName);

		// -(BOOL)removeBO:(NSString * _Nonnull)boId;
		[Export ("removeBO:")]
		bool RemoveBO (string boId);

		// -(BOOL)assignUser:(NSString * _Nonnull)boUserId toBO:(NSString * _Nonnull)boId;
		[Export ("assignUser:toBO:")]
		bool AssignUser (string boUserId, string boId);

		// -(BOOL)removeUser:(NSString * _Nonnull)boUserId fromBO:(NSString * _Nonnull)boId;
		[Export ("removeUser:fromBO:")]
		bool RemoveUser (string boUserId, string boId);

		// -(BOOL)setBOOption:(MobileRTCBOOption * _Nonnull)option;
		[Export ("setBOOption:")]
		bool SetBOOption (MobileRTCBOOption option);

		// -(MobileRTCBOOption * _Nullable)getBOOption;
		[NullAllowed, Export ("getBOOption")]
		MobileRTCBOOption BOOption { get; }

		// -(BOOL)isWebPreAssignBOEnabled;
		[Export ("isWebPreAssignBOEnabled")]
		bool IsWebPreAssignBOEnabled { get; }

		// -(MobileRTCSDKError)requestAndUseWebPreAssignBOList;
		[Export ("requestAndUseWebPreAssignBOList")]
		MobileRTCSDKError RequestAndUseWebPreAssignBOList { get; }

		// -(MobileRTCBOPreAssignBODataStatus)getWebPreAssignBODataStatus;
		[Export ("getWebPreAssignBODataStatus")]
		MobileRTCBOPreAssignBODataStatus WebPreAssignBODataStatus { get; }
	}

	// @interface MobileRTCBOAdmin : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCBOAdmin
	{
		// -(BOOL)startBO;
		[Export ("startBO")]
		bool StartBO { get; }

		// -(BOOL)stopBO;
		[Export ("stopBO")]
		bool StopBO { get; }

		// -(BOOL)assignNewUser:(NSString * _Nonnull)boUserId toRunningBO:(NSString * _Nonnull)boId;
		[Export ("assignNewUser:toRunningBO:")]
		bool AssignNewUser (string boUserId, string boId);

		// -(BOOL)switchUser:(NSString * _Nonnull)boUserId toRunningBO:(NSString * _Nonnull)boId;
		[Export ("switchUser:toRunningBO:")]
		bool SwitchUser (string boUserId, string boId);

		// -(BOOL)canStartBO;
		[Export ("canStartBO")]
		bool CanStartBO { get; }

		// -(BOOL)joinBOByUserRequest:(NSString * _Nonnull)boUserId;
		[Export ("joinBOByUserRequest:")]
		bool JoinBOByUserRequest (string boUserId);

		// -(BOOL)ignoreUserHelpRequest:(NSString * _Nonnull)boUserId;
		[Export ("ignoreUserHelpRequest:")]
		bool IgnoreUserHelpRequest (string boUserId);

		// -(BOOL)broadcastMessage:(NSString * _Nonnull)strMsg;
		[Export ("broadcastMessage:")]
		bool BroadcastMessage (string strMsg);

		// -(BOOL)inviteBOUserReturnToMainSession:(NSString * _Nonnull)boUserId;
		[Export ("inviteBOUserReturnToMainSession:")]
		bool InviteBOUserReturnToMainSession (string boUserId);

		// -(BOOL)isBroadcastVoiceToBOSupport;
		[Export ("isBroadcastVoiceToBOSupport")]
		bool IsBroadcastVoiceToBOSupport { get; }

		// -(BOOL)canBroadcastVoiceToBO;
		[Export ("canBroadcastVoiceToBO")]
		bool CanBroadcastVoiceToBO { get; }

		// -(BOOL)broadcastVoiceToBO:(BOOL)bStart;
		[Export ("broadcastVoiceToBO:")]
		bool BroadcastVoiceToBO (bool bStart);
	}

	// @interface MobileRTCBOAssistant : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCBOAssistant
	{
		// -(BOOL)joinBO:(NSString * _Nonnull)boId;
		[Export ("joinBO:")]
		bool JoinBO (string boId);

		// -(BOOL)leaveBO;
		[Export ("leaveBO")]
		bool LeaveBO { get; }
	}

	// @interface MobileRTCBOAttendee : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCBOAttendee
	{
		// -(BOOL)joinBO;
		[Export ("joinBO")]
		bool JoinBO { get; }

		// -(BOOL)leaveBO;
		[Export ("leaveBO")]
		bool LeaveBO { get; }

		// -(NSString * _Nullable)getBOName;
		[NullAllowed, Export ("getBOName")]
		string BOName { get; }

		// -(BOOL)requestForHelp;
		[Export ("requestForHelp")]
		bool RequestForHelp { get; }

		// -(BOOL)isHostInThisBO;
		[Export ("isHostInThisBO")]
		bool IsHostInThisBO { get; }

		// -(BOOL)isCanReturnMainSession;
		[Export ("isCanReturnMainSession")]
		bool IsCanReturnMainSession { get; }
	}

	// @interface MobileRTCBOData : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCBOData
	{
		// -(NSArray * _Nullable)getUnassignedUserList;
		[NullAllowed, Export ("getUnassignedUserList")]
		//[Verify (MethodToProperty), Verify (StronglyTypedNSArray)]
		NSObject[] UnassignedUserList { get; }

		// -(NSArray * _Nullable)getBOMeetingIDList;
		[NullAllowed, Export ("getBOMeetingIDList")]
		//[Verify (MethodToProperty), Verify (StronglyTypedNSArray)]
		NSObject[] BOMeetingIDList { get; }

		// -(MobileRTCBOUser * _Nullable)getBOUserByUserID:(NSString * _Nonnull)userId;
		[Export ("getBOUserByUserID:")]
		[return: NullAllowed]
		MobileRTCBOUser GetBOUserByUserID (string userId);

		// -(MobileRTCBOMeeting * _Nullable)getBOMeetingByID:(NSString * _Nonnull)boId;
		[Export ("getBOMeetingByID:")]
		[return: NullAllowed]
		MobileRTCBOMeeting GetBOMeetingByID (string boId);

		// -(NSString * _Nullable)getCurrentBOName;
		[NullAllowed, Export ("getCurrentBOName")]
		string CurrentBOName { get; }

		// -(BOOL)isBOUserMyself:(NSString * _Nonnull)boUserId;
		[Export ("isBOUserMyself:")]
		bool IsBOUserMyself (string boUserId);
	}

	// @interface MobileRTCReturnToMainSessionHandler : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCReturnToMainSessionHandler
	{
		// -(BOOL)returnToMainSession;
		[Export ("returnToMainSession")]
		bool ReturnToMainSession { get; }

		// -(void)ignore;
		[Export ("ignore")]
		void Ignore ();
	}

	// @interface MobileRTCPreProcessRawData : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCPreProcessRawData
	{
		// @property (assign, nonatomic) CGSize size;
		[Export ("size", ArgumentSemantic.Assign)]
		CGSize Size { get; set; }

		// @property (assign, nonatomic) int yStride;
		[Export ("yStride")]
		int YStride { get; set; }

		// @property (assign, nonatomic) int uStride;
		[Export ("uStride")]
		int UStride { get; set; }

		// @property (assign, nonatomic) int vStride;
		[Export ("vStride")]
		int VStride { get; set; }

		// -(char *)getYBuffer:(int)lineNum;
		[Export ("getYBuffer:")]
		unsafe IntPtr GetYBuffer (int lineNum);

		// -(char *)getUBuffer:(int)lineNum;
		[Export ("getUBuffer:")]
		unsafe IntPtr GetUBuffer (int lineNum);

		// -(char *)getVBuffer:(int)lineNum;
		[Export ("getVBuffer:")]
		unsafe IntPtr GetVBuffer (int lineNum);

		// @property (assign, nonatomic) MobileRTCFrameDataFormat format;
		[Export ("format", ArgumentSemantic.Assign)]
		MobileRTCFrameDataFormat Format { get; set; }

		// @property (assign, nonatomic) MobileRTCVideoRawDataRotation rotation;
		[Export ("rotation", ArgumentSemantic.Assign)]
		MobileRTCVideoRawDataRotation Rotation { get; set; }
	}

	// @interface MobileRTCAudioSender : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCAudioSender
	{
		// -(MobileRTCRawDataError)send:(char *)data dataLength:(unsigned int)length sampleRate:(int)rate;
		[Export ("send:dataLength:sampleRate:")]
		unsafe MobileRTCRawDataError Send (IntPtr data, uint length, int rate);
	}

	// @interface MobileRTCVideoSender : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCVideoSender
	{
		// -(void)sendVideoFrame:(char *)frameBuffer width:(NSUInteger)width height:(NSUInteger)height dataLength:(NSUInteger)dataLength rotation:(MobileRTCVideoRawDataRotation)rotation __attribute__((deprecated("Use -sendRawData: width: height: dataLength: ratation: format: instead")));
		[Export ("sendVideoFrame:width:height:dataLength:rotation:")]
		unsafe void SendVideoFrame (IntPtr frameBuffer, nuint width, nuint height, nuint dataLength, MobileRTCVideoRawDataRotation rotation);

		// -(void)sendVideoFrame:(char *)frameBuffer width:(NSUInteger)width height:(NSUInteger)height dataLength:(NSUInteger)dataLength rotation:(MobileRTCVideoRawDataRotation)rotation format:(MobileRTCFrameDataFormat)format;
		[Export ("sendVideoFrame:width:height:dataLength:rotation:format:")]
		unsafe void SendVideoFrame (IntPtr frameBuffer, nuint width, nuint height, nuint dataLength, MobileRTCVideoRawDataRotation rotation, MobileRTCFrameDataFormat format);
	}

	// @interface MobileRTCShareSender : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCShareSender
	{
		// -(void)sendShareFrameBuffer:(char *)frameBuffer width:(NSUInteger)width height:(NSUInteger)height frameLength:(NSUInteger)dataLength __attribute__((deprecated("Use -sendShareFrameBuffer:width:height:frameLength:format:instead")));
		[Export ("sendShareFrameBuffer:width:height:frameLength:")]
		unsafe void SendShareFrameBuffer (IntPtr frameBuffer, nuint width, nuint height, nuint dataLength);

		// -(void)sendShareFrameBuffer:(char *)frameBuffer width:(NSUInteger)width height:(NSUInteger)height frameLength:(NSUInteger)dataLength format:(MobileRTCFrameDataFormat)format;
		[Export ("sendShareFrameBuffer:width:height:frameLength:format:")]
		unsafe void SendShareFrameBuffer (IntPtr frameBuffer, nuint width, nuint height, nuint dataLength, MobileRTCFrameDataFormat format);
	}

	// @interface MobileRTCVideoCapabilityItem : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCVideoCapabilityItem
	{
		// @property (assign, nonatomic) int width;
		[Export ("width")]
		int Width { get; set; }

		// @property (assign, nonatomic) int height;
		[Export ("height")]
		int Height { get; set; }

		// @property (assign, nonatomic) int videoFrame;
		[Export ("videoFrame")]
		int VideoFrame { get; set; }
	}

	// @interface MobileRTCLiveTranscriptionLanguage : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCLiveTranscriptionLanguage
	{
		// @property (readonly, assign, nonatomic) NSInteger languageID;
		[Export ("languageID")]
		nint LanguageID { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull languageName;
		[Export ("languageName")]
		string LanguageName { get; }
	}

	// @interface MobileRTCRawLiveStreamInfo : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCRawLiveStreamInfo
	{
		// @property (readonly, assign, nonatomic) NSUInteger userId;
		[Export ("userId")]
		nuint UserId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable broadcastUrl;
		[NullAllowed, Export ("broadcastUrl")]
		string BroadcastUrl { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable broadcastName;
		[NullAllowed, Export ("broadcastName")]
		string BroadcastName { get; }
	}

	// @interface MobileRTCRequestRawLiveStreamPrivilegeHandler : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCRequestRawLiveStreamPrivilegeHandler
	{
		// -(NSString *)getRequestId;
		[Export ("getRequestId")]
		string RequestId { get; }

		// -(NSUInteger)getRequesterId;
		[Export ("getRequesterId")]
		nuint RequesterId { get; }

		// -(NSString *)getRequesterName;
		[Export ("getRequesterName")]
		string RequesterName { get; }

		// -(NSString *)getBroadcastUrl;
		[Export ("getBroadcastUrl")]
		string BroadcastUrl { get; }

		// -(NSString *)getBroadcastName;
		[Export ("getBroadcastName")]
		string BroadcastName { get; }

		// -(BOOL)grantRawLiveStreamPrivilege;
		[Export ("grantRawLiveStreamPrivilege")]
		bool GrantRawLiveStreamPrivilege { get; }

		// -(BOOL)denyRawLiveStreamPrivilege;
		[Export ("denyRawLiveStreamPrivilege")]
		bool DenyRawLiveStreamPrivilege { get; }
	}

	// @interface MobileRTCShareAudioSender : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCShareAudioSender
	{
		// -(MobileRTCRawDataError)sendShareAudio:(char *)data dataLength:(NSUInteger)length sampleRate:(NSUInteger)rate audioChannel:(MobileRTCAudioChannel)channel;
		[Export ("sendShareAudio:dataLength:sampleRate:audioChannel:")]
		unsafe MobileRTCRawDataError SendShareAudio (IntPtr data, nuint length, nuint rate, MobileRTCAudioChannel channel);
	}

	// @protocol MobileRTCMeetingServiceDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface IMobileRTCMeetingServiceDelegate
	{
		// @optional -(void)onMeetingError:(MobileRTCMeetError)error message:(NSString * _Nullable)message;
		[Export ("onMeetingError:message:")]
		void OnMeetingError (MobileRTCMeetError error, [NullAllowed] string message);

		// @optional -(void)onMeetingStateChange:(MobileRTCMeetingState)state;
		[Export ("onMeetingStateChange:")]
		void OnMeetingStateChange (MobileRTCMeetingState state);

		// @optional -(void)onMeetingParameterNotification:(MobileRTCMeetingParameter * _Nullable)meetingParam;
		[Export ("onMeetingParameterNotification:")]
		void OnMeetingParameterNotification ([NullAllowed] MobileRTCMeetingParameter meetingParam);

		// @optional -(void)onJoinMeetingConfirmed;
		[Export ("onJoinMeetingConfirmed")]
		void OnJoinMeetingConfirmed ();

		// @optional -(void)onMeetingReady;
		[Export ("onMeetingReady")]
		void OnMeetingReady ();

		// @optional -(void)onJBHWaitingWithCmd:(JBHCmd)cmd;
		[Export ("onJBHWaitingWithCmd:")]
		void OnJBHWaitingWithCmd (JBHCmd cmd);

		// @optional -(void)onCheckCMRPrivilege:(MobileRTCCMRError)result;
		[Export ("onCheckCMRPrivilege:")]
		void OnCheckCMRPrivilege (MobileRTCCMRError result);

		// @optional -(void)onRecordingStatus:(MobileRTCRecordingStatus)status;
		[Export ("onRecordingStatus:")]
		void OnRecordingStatus (MobileRTCRecordingStatus status);

		// @optional -(void)onLocalRecordingStatus:(NSInteger)userId status:(MobileRTCRecordingStatus)status;
		[Export ("onLocalRecordingStatus:status:")]
		void OnLocalRecordingStatus (nint userId, MobileRTCRecordingStatus status);

		// @optional -(void)onMeetingEndedReason:(MobileRTCMeetingEndReason)reason;
		[Export ("onMeetingEndedReason:")]
		void OnMeetingEndedReason (MobileRTCMeetingEndReason reason);

		// @optional -(void)onNoHostMeetingWillTerminate:(NSUInteger)minutes;
		[Export ("onNoHostMeetingWillTerminate:")]
		void OnNoHostMeetingWillTerminate (nuint minutes);

		// @optional -(void)onMicrophoneStatusError:(MobileRTCMicrophoneError)error;
		[Export ("onMicrophoneStatusError:")]
		void OnMicrophoneStatusError (MobileRTCMicrophoneError error);

		// @optional -(void)onJoinMeetingInfo:(MobileRTCJoinMeetingInfo)info completion:(void (^ _Nonnull)(NSString * _Nonnull, NSString * _Nonnull, BOOL))completion;
		[Export ("onJoinMeetingInfo:completion:")]
		void OnJoinMeetingInfo (MobileRTCJoinMeetingInfo info, Action<NSString, NSString, bool> completion);

		// @optional -(void)onProxyAuth:(NSString * _Nonnull)host port:(NSUInteger)port completion:(void (^ _Nonnull)(NSString * _Nonnull, NSUInteger, NSString * _Nonnull, NSString * _Nonnull, BOOL))completion;
		[Export ("onProxyAuth:port:completion:")]
		void OnProxyAuth (string host, nuint port, Action<NSString, nuint, NSString, NSString, bool> completion);

		// @optional -(void)onAskToEndOtherMeeting:(void (^ _Nonnull)(BOOL))completion;
		[Export ("onAskToEndOtherMeeting:")]
		void OnAskToEndOtherMeeting (Action<bool> completion);

		// @optional -(void)onMicrophoneNoPrivilege;
		[Export ("onMicrophoneNoPrivilege")]
		void OnMicrophoneNoPrivilege ();

		// @optional -(void)onCameraNoPrivilege;
		[Export ("onCameraNoPrivilege")]
		void OnCameraNoPrivilege ();

		// @optional -(void)onUpgradeFreeMeetingResult:(NSUInteger)result;
		[Export ("onUpgradeFreeMeetingResult:")]
		void OnUpgradeFreeMeetingResult (nuint result);

		// @optional -(void)onFreeMeetingNeedToUpgrade:(FreeMeetingNeedUpgradeType)type giftUpgradeURL:(NSString * _Nullable)giftURL;
		[Export ("onFreeMeetingNeedToUpgrade:giftUpgradeURL:")]
		void OnFreeMeetingNeedToUpgrade (FreeMeetingNeedUpgradeType type, [NullAllowed] string giftURL);

		// @optional -(void)onFreeMeetingUpgradeToGiftFreeTrialStart;
		[Export ("onFreeMeetingUpgradeToGiftFreeTrialStart")]
		void OnFreeMeetingUpgradeToGiftFreeTrialStart ();

		// @optional -(void)onFreeMeetingUpgradeToGiftFreeTrialStop;
		[Export ("onFreeMeetingUpgradeToGiftFreeTrialStop")]
		void OnFreeMeetingUpgradeToGiftFreeTrialStop ();

		// @optional -(void)onFreeMeetingUpgradedToProMeeting;
		[Export ("onFreeMeetingUpgradedToProMeeting")]
		void OnFreeMeetingUpgradedToProMeeting ();

		//// @optional -(BOOL)onClickedInviteButton:(UIViewController * _Nonnull)parentVC addInviteActionItem:(NSMutableArray<MobileRTCMeetingInviteActionItem *> * _Nullable)array;
        [Export("onClickedInviteButton:addInviteActionItem:")]
        bool OnClickedInviteButton(UIViewController parentVC, [NullAllowed] NSMutableArray array);

		// @optional -(BOOL)onClickedAudioButton:(UIViewController * _Nonnull)parentVC;
		[Export ("onClickedAudioButton:")]
		bool OnClickedAudioButton (UIViewController parentVC);

		// @optional -(BOOL)onClickedParticipantsButton:(UIViewController * _Nonnull)parentVC;
		[Export ("onClickedParticipantsButton:")]
		bool OnClickedParticipantsButton (UIViewController parentVC);

		//// @optional -(BOOL)onClickedShareButton:(UIViewController * _Nonnull)parentVC addShareActionItem:(NSMutableArray<MobileRTCMeetingShareActionItem *> * _Nonnull)array;
        [Export("onClickedShareButton:addShareActionItem:")]
        bool OnClickedShareButton(UIViewController parentVC, NSMutableArray array);

		// @optional -(BOOL)onClickedEndButton:(UIViewController * _Nonnull)parentVC endButton:(UIButton * _Nonnull)endButton;
		[Export ("onClickedEndButton:endButton:")]
		bool OnClickedEndButton (UIViewController parentVC, UIButton endButton);

		// @optional -(BOOL)onCheckIfMeetingVoIPCallRunning;
		[Export ("onCheckIfMeetingVoIPCallRunning")]
		bool OnCheckIfMeetingVoIPCallRunning { get; }

		// @optional -(void)onOngoingShareStopped;
		[Export ("onOngoingShareStopped")]
		void OnOngoingShareStopped ();

		// @optional -(void)onClickedDialOut:(UIViewController * _Nonnull)parentVC isCallMe:(BOOL)me;
		[Export ("onClickedDialOut:isCallMe:")]
		void OnClickedDialOut (UIViewController parentVC, bool me);

		// @optional -(void)onDialOutStatusChanged:(DialOutStatus)status;
		[Export ("onDialOutStatusChanged:")]
		void OnDialOutStatusChanged (DialOutStatus status);

		// @optional -(void)onSendPairingCodeStateChanged:(MobileRTCH323ParingStatus)state MeetingNumber:(unsigned long long)meetingNumber;
		[Export ("onSendPairingCodeStateChanged:MeetingNumber:")]
		void OnSendPairingCodeStateChanged (MobileRTCH323ParingStatus state, ulong meetingNumber);

		// @optional -(void)onCallRoomDeviceStateChanged:(H323CallOutStatus)state;
		[Export ("onCallRoomDeviceStateChanged:")]
		void OnCallRoomDeviceStateChanged (H323CallOutStatus state);

		// @optional -(void)onInMeetingChat:(NSString * _Nonnull)messageID;
		[Export ("onInMeetingChat:")]
		void OnInMeetingChat (string messageID);

		// @optional -(void)onChatMsgDeleteNotification:(NSString * _Nonnull)msgID deleteBy:(MobileRTCChatMessageDeleteType)deleteBy;
		[Export ("onChatMsgDeleteNotification:deleteBy:")]
		void OnChatMsgDeleteNotification (string msgID, MobileRTCChatMessageDeleteType deleteBy);

		// @optional -(void)onLiveStreamStatusChange:(MobileRTCLiveStreamStatus)liveStreamStatus;
		[Export ("onLiveStreamStatusChange:")]
		void OnLiveStreamStatusChange (MobileRTCLiveStreamStatus liveStreamStatus);

		// @optional -(void)onRawLiveStreamPrivilegeChanged:(BOOL)hasPrivilege;
		[Export ("onRawLiveStreamPrivilegeChanged:")]
		void OnRawLiveStreamPrivilegeChanged (bool hasPrivilege);

		// @optional -(void)onRawLiveStreamPrivilegeRequestTimeout;
		[Export ("onRawLiveStreamPrivilegeRequestTimeout")]
		void OnRawLiveStreamPrivilegeRequestTimeout ();

		// @optional -(void)onUserRawLiveStreamPrivilegeChanged:(NSUInteger)userId hasPrivilege:(_Bool)hasPrivilege;
		[Export ("onUserRawLiveStreamPrivilegeChanged:hasPrivilege:")]
		void OnUserRawLiveStreamPrivilegeChanged (nuint userId, bool hasPrivilege);

		// @optional -(void)onRawLiveStreamPrivilegeRequested:(MobileRTCRequestRawLiveStreamPrivilegeHandler * _Nullable)handler;
		[Export ("onRawLiveStreamPrivilegeRequested:")]
		void OnRawLiveStreamPrivilegeRequested ([NullAllowed] MobileRTCRequestRawLiveStreamPrivilegeHandler handler);

		// @optional -(void)onUserRawLiveStreamingStatusChanged:(NSArray<MobileRTCRawLiveStreamInfo *> * _Nullable)liveStreamList;
		[Export ("onUserRawLiveStreamingStatusChanged:")]
		void OnUserRawLiveStreamingStatusChanged ([NullAllowed] MobileRTCRawLiveStreamInfo[] liveStreamList);

		// @optional -(void)onZoomIdentityExpired;
		[Export ("onZoomIdentityExpired")]
		void OnZoomIdentityExpired ();

		// @optional -(void)onClickShareScreen:(UIViewController * _Nonnull)parentVC;
		[Export ("onClickShareScreen:")]
		void OnClickShareScreen (UIViewController parentVC);

		// @optional -(void)onClosedCaptionReceived:(NSString * _Nonnull)message speakerId:(NSUInteger)speakerID msgTime:(NSDate * _Nullable)msgTime;
		[Export ("onClosedCaptionReceived:speakerId:msgTime:")]
		void OnClosedCaptionReceived (string message, nuint speakerID, [NullAllowed] NSDate msgTime);

		// @optional -(void)onWaitingRoomStatusChange:(BOOL)needWaiting;
		[Export ("onWaitingRoomStatusChange:")]
		void OnWaitingRoomStatusChange (bool needWaiting);

		// @optional -(void)onSinkAttendeeChatPriviledgeChanged:(MobileRTCMeetingChatPriviledgeType)currentPrivilege;
		[Export ("onSinkAttendeeChatPriviledgeChanged:")]
		void OnSinkAttendeeChatPriviledgeChanged (MobileRTCMeetingChatPriviledgeType currentPrivilege);

		// @optional -(void)onSinkPanelistChatPrivilegeChanged:(MobileRTCPanelistChatPrivilegeType)privilege;
		[Export ("onSinkPanelistChatPrivilegeChanged:")]
		void OnSinkPanelistChatPrivilegeChanged (MobileRTCPanelistChatPrivilegeType privilege);

		// @optional -(void)onSubscribeUserFail:(MobileRTCSubscribeFailReason)errorCode size:(NSInteger)size userId:(NSUInteger)userId;
		[Export ("onSubscribeUserFail:size:userId:")]
		void OnSubscribeUserFail (MobileRTCSubscribeFailReason errorCode, nint size, nuint userId);

		// @optional -(void)onRequestLocalRecordingPrivilegeReceived:(MobileRTCRequestLocalRecordingPrivilegeHandler * _Nullable)handler;
		[Export ("onRequestLocalRecordingPrivilegeReceived:")]
		void OnRequestLocalRecordingPrivilegeReceived ([NullAllowed] MobileRTCRequestLocalRecordingPrivilegeHandler handler);

		// @optional -(void)onSuspendParticipantsActivities;
		[Export ("onSuspendParticipantsActivities")]
		void OnSuspendParticipantsActivities ();

		// @optional -(void)onAllowParticipantsStartVideoNotification:(BOOL)allow;
		[Export ("onAllowParticipantsStartVideoNotification:")]
		void OnAllowParticipantsStartVideoNotification (bool allow);

		// @optional -(void)onAllowParticipantsRenameNotification:(BOOL)allow;
		[Export ("onAllowParticipantsRenameNotification:")]
		void OnAllowParticipantsRenameNotification (bool allow);

		// @optional -(void)onAllowParticipantsUnmuteSelfNotification:(BOOL)allow;
		[Export ("onAllowParticipantsUnmuteSelfNotification:")]
		void OnAllowParticipantsUnmuteSelfNotification (bool allow);

		// @optional -(void)onAllowParticipantsShareWhiteBoardNotification:(BOOL)allow;
		[Export ("onAllowParticipantsShareWhiteBoardNotification:")]
		void OnAllowParticipantsShareWhiteBoardNotification (bool allow);

		// @optional -(void)onAllowParticipantsShareStatusNotification:(BOOL)allow;
		[Export ("onAllowParticipantsShareStatusNotification:")]
		void OnAllowParticipantsShareStatusNotification (bool allow);

		// @optional -(void)onMeetingLockStatus:(BOOL)isLock;
		[Export ("onMeetingLockStatus:")]
		void OnMeetingLockStatus (bool isLock);

		// @optional -(void)onRequestLocalRecordingPrivilegeChanged:(MobileRTCLocalRecordingRequestPrivilegeStatus)status;
		[Export ("onRequestLocalRecordingPrivilegeChanged:")]
		void OnRequestLocalRecordingPrivilegeChanged (MobileRTCLocalRecordingRequestPrivilegeStatus status);
	}

	// @protocol MobileRTCAudioServiceDelegate <MobileRTCMeetingServiceDelegate>
	[Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MobileRTCAudioServiceDelegate : IMobileRTCMeetingServiceDelegate
	{
		// @optional -(void)onSinkMeetingAudioStatusChange:(NSUInteger)userID;
		[Export ("onSinkMeetingAudioStatusChange:")]
		void OnSinkMeetingAudioStatusChange (nuint userID);

		// @optional -(void)onSinkMeetingMyAudioTypeChange;
		[Export ("onSinkMeetingMyAudioTypeChange")]
		void OnSinkMeetingMyAudioTypeChange ();

		// @optional -(void)onSinkMeetingAudioTypeChange:(NSUInteger)userID;
		[Export ("onSinkMeetingAudioTypeChange:")]
		void OnSinkMeetingAudioTypeChange (nuint userID);

		// @optional -(void)onSinkMeetingAudioStatusChange:(NSUInteger)userID audioStatus:(MobileRTC_AudioStatus)audioStatus;
		[Export ("onSinkMeetingAudioStatusChange:audioStatus:")]
		void OnSinkMeetingAudioStatusChange (nuint userID, MobileRTC_AudioStatus audioStatus);

		// @optional -(void)onAudioOutputChange;
		[Export ("onAudioOutputChange")]
		void OnAudioOutputChange ();

		// @optional -(void)onMyAudioStateChange;
		[Export ("onMyAudioStateChange")]
		void OnMyAudioStateChange ();

		// @optional -(void)onSinkMeetingAudioRequestUnmuteByHost;
		[Export ("onSinkMeetingAudioRequestUnmuteByHost")]
		void OnSinkMeetingAudioRequestUnmuteByHost ();
	}

	// @protocol MobileRTCVideoServiceDelegate <MobileRTCMeetingServiceDelegate>
	[Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MobileRTCVideoServiceDelegate : IMobileRTCMeetingServiceDelegate
	{
		// @optional -(void)onSinkMeetingActiveVideo:(NSUInteger)userID;
		[Export ("onSinkMeetingActiveVideo:")]
		void OnSinkMeetingActiveVideo (nuint userID);

		// @optional -(void)onSinkMeetingVideoStatusChange:(NSUInteger)userID;
		[Export ("onSinkMeetingVideoStatusChange:")]
		void OnSinkMeetingVideoStatusChange (nuint userID);

		// @optional -(void)onMyVideoStateChange;
		[Export ("onMyVideoStateChange")]
		void OnMyVideoStateChange ();

		// @optional -(void)onSinkMeetingVideoStatusChange:(NSUInteger)userID videoStatus:(MobileRTC_VideoStatus)videoStatus;
		[Export ("onSinkMeetingVideoStatusChange:videoStatus:")]
		void OnSinkMeetingVideoStatusChange (nuint userID, MobileRTC_VideoStatus videoStatus);

		// @optional -(void)onSpotlightVideoChange:(BOOL)on;
		[Export ("onSpotlightVideoChange:")]
		void OnSpotlightVideoChange (bool on);

		// @optional -(void)onSpotlightVideoUserChange:(NSArray<NSNumber *> * _Nullable)spotlightedUserList;
		[Export ("onSpotlightVideoUserChange:")]
		void OnSpotlightVideoUserChange ([NullAllowed] NSNumber[] spotlightedUserList);

		// @optional -(void)onSinkMeetingPreviewStopped;
		[Export ("onSinkMeetingPreviewStopped")]
		void OnSinkMeetingPreviewStopped ();

		// @optional -(void)onSinkMeetingActiveVideoForDeck:(NSUInteger)userID;
		[Export ("onSinkMeetingActiveVideoForDeck:")]
		void OnSinkMeetingActiveVideoForDeck (nuint userID);

		// @optional -(void)onSinkMeetingVideoQualityChanged:(MobileRTCVideoQuality)qality userID:(NSUInteger)userID;
		[Export ("onSinkMeetingVideoQualityChanged:userID:")]
		void OnSinkMeetingVideoQualityChanged (MobileRTCVideoQuality qality, nuint userID);

		// @optional -(void)onSinkMeetingVideoRequestUnmuteByHost:(void (^ _Nonnull)(BOOL))completion;
		[Export ("onSinkMeetingVideoRequestUnmuteByHost:")]
		void OnSinkMeetingVideoRequestUnmuteByHost (Action<bool> completion);

		// @optional -(void)onSinkMeetingShowMinimizeMeetingOrBackZoomUI:(MobileRTCMinimizeMeetingState)state;
		[Export ("onSinkMeetingShowMinimizeMeetingOrBackZoomUI:")]
		void OnSinkMeetingShowMinimizeMeetingOrBackZoomUI (MobileRTCMinimizeMeetingState state);

		// @optional -(void)onHostVideoOrderUpdated:(NSArray<NSNumber *> * _Nullable)orderArr;
		[Export ("onHostVideoOrderUpdated:")]
		void OnHostVideoOrderUpdated ([NullAllowed] NSNumber[] orderArr);

		// @optional -(void)onLocalVideoOrderUpdated:(NSArray<NSNumber *> * _Nullable)localOrderArr;
		[Export ("onLocalVideoOrderUpdated:")]
		void OnLocalVideoOrderUpdated ([NullAllowed] NSNumber[] localOrderArr);

		// @optional -(void)onFollowHostVideoOrderChanged:(BOOL)follow;
		[Export ("onFollowHostVideoOrderChanged:")]
		void OnFollowHostVideoOrderChanged (bool follow);
	}

	// @protocol MobileRTCUserServiceDelegate <MobileRTCMeetingServiceDelegate>
	[Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MobileRTCUserServiceDelegate : IMobileRTCMeetingServiceDelegate
	{
		// @optional -(void)onMyHandStateChange;
		[Export ("onMyHandStateChange")]
		void OnMyHandStateChange ();

		// @optional -(void)onInMeetingUserUpdated;
		[Export ("onInMeetingUserUpdated")]
		void OnInMeetingUserUpdated ();

		// @optional -(void)onInMeetingUserAvatarPathUpdated:(NSInteger)userID;
		[Export ("onInMeetingUserAvatarPathUpdated:")]
		void OnInMeetingUserAvatarPathUpdated (nint userID);

		// @optional -(void)onSinkMeetingUserJoin:(NSUInteger)userID;
		[Export ("onSinkMeetingUserJoin:")]
		void OnSinkMeetingUserJoin (nuint userID);

		// @optional -(void)onSinkMeetingUserLeft:(NSUInteger)userID;
		[Export ("onSinkMeetingUserLeft:")]
		void OnSinkMeetingUserLeft (nuint userID);

		// @optional -(void)onSinkMeetingUserRaiseHand:(NSUInteger)userID;
		[Export ("onSinkMeetingUserRaiseHand:")]
		void OnSinkMeetingUserRaiseHand (nuint userID);

		// @optional -(void)onSinkMeetingUserLowerHand:(NSUInteger)userID;
		[Export ("onSinkMeetingUserLowerHand:")]
		void OnSinkMeetingUserLowerHand (nuint userID);

		// @optional -(void)onSinkLowerAllHands;
		[Export ("onSinkLowerAllHands")]
		void OnSinkLowerAllHands ();

		// @optional -(void)onSinkUserNameChanged:(NSArray<NSNumber *> * _Nullable)userNameChangedArr;
		[Export ("onSinkUserNameChanged:")]
		void OnSinkUserNameChanged ([NullAllowed] NSNumber[] userNameChangedArr);

		// @optional -(void)onMeetingHostChange:(NSUInteger)hostId;
		[Export ("onMeetingHostChange:")]
		void OnMeetingHostChange (nuint hostId);

		// @optional -(void)onMeetingCoHostChange:(NSUInteger)userID isCoHost:(BOOL)isCoHost;
		[Export ("onMeetingCoHostChange:isCoHost:")]
		void OnMeetingCoHostChange (nuint userID, bool isCoHost);

		// @optional -(void)onClaimHostResult:(MobileRTCClaimHostError)error;
		[Export ("onClaimHostResult:")]
		void OnClaimHostResult (MobileRTCClaimHostError error);
	}

	// @protocol MobileRTCShareServiceDelegate <MobileRTCMeetingServiceDelegate>
	[Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MobileRTCShareServiceDelegate : IMobileRTCMeetingServiceDelegate
	{
		// @optional -(void)onAppShareSplash;
		[Export ("onAppShareSplash")]
		void OnAppShareSplash ();

		// @optional -(void)onSinkSharingStatus:(MobileRTCSharingStatus)status userID:(NSUInteger)userID;
		[Export ("onSinkSharingStatus:userID:")]
		void OnSinkSharingStatus (MobileRTCSharingStatus status, nuint userID);

		// @optional -(void)onSinkShareSettingTypeChanged:(MobileRTCShareSettingType)shareSettingType;
		[Export ("onSinkShareSettingTypeChanged:")]
		void OnSinkShareSettingTypeChanged (MobileRTCShareSettingType shareSettingType);

		// @optional -(void)onSinkShareSizeChange:(NSUInteger)userID;
		[Export ("onSinkShareSizeChange:")]
		void OnSinkShareSizeChange (nuint userID);
	}

	// @protocol MobileRTCInterpretationServiceDelegate <MobileRTCMeetingServiceDelegate>
	[Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MobileRTCInterpretationServiceDelegate : IMobileRTCMeetingServiceDelegate
	{
		// @optional -(void)onInterpretationStart;
		[Export ("onInterpretationStart")]
		void OnInterpretationStart ();

		// @optional -(void)onInterpretationStop;
		[Export ("onInterpretationStop")]
		void OnInterpretationStop ();

		// @optional -(void)onInterpreterListChanged;
		[Export ("onInterpreterListChanged")]
		void OnInterpreterListChanged ();

		// @optional -(void)onInterpreterRoleChanged:(NSUInteger)userID isInterpreter:(BOOL)isInterpreter;
		[Export ("onInterpreterRoleChanged:isInterpreter:")]
		void OnInterpreterRoleChanged (nuint userID, bool isInterpreter);

		// @optional -(void)onInterpreterActiveLanguageChanged:(NSInteger)userID activeLanguageId:(NSInteger)activeLanID;
		[Export ("onInterpreterActiveLanguageChanged:activeLanguageId:")]
		void OnInterpreterActiveLanguageChanged (nint userID, nint activeLanID);

		// @optional -(void)onInterpreterLanguageChanged:(NSInteger)lanID1 andLanguage2:(NSInteger)lanID2;
		[Export ("onInterpreterLanguageChanged:andLanguage2:")]
		void OnInterpreterLanguageChanged (nint lanID1, nint lanID2);

		// @optional -(void)onAvailableLanguageListUpdated:(NSArray<MobileRTCInterpretationLanguage *> * _Nullable)availableLanguageList;
		[Export ("onAvailableLanguageListUpdated:")]
		void OnAvailableLanguageListUpdated ([NullAllowed] MobileRTCInterpretationLanguage[] availableLanguageList);

		// @optional -(void)onInterpreterLanguagesUpdated:(NSArray<MobileRTCInterpretationLanguage *> * _Nullable)availableLanguages;
		[Export ("onInterpreterLanguagesUpdated:")]
		void OnInterpreterLanguagesUpdated ([NullAllowed] MobileRTCInterpretationLanguage[] availableLanguages);
	}

	// @protocol MobileRTCSignInterpretationServiceDelegate <MobileRTCMeetingServiceDelegate>
	[Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MobileRTCSignInterpretationServiceDelegate : IMobileRTCMeetingServiceDelegate
	{
		// @optional -(void)onSignInterpretationStatusChange:(MobileRTCSignInterpretationStatus)status;
		[Export ("onSignInterpretationStatusChange:")]
		void OnSignInterpretationStatusChange (MobileRTCSignInterpretationStatus status);

		// @optional -(void)onSignInterpreterListChanged;
		[Export ("onSignInterpreterListChanged")]
		void OnSignInterpreterListChanged ();

		// @optional -(void)onSignInterpreterRoleChanged;
		[Export ("onSignInterpreterRoleChanged")]
		void OnSignInterpreterRoleChanged ();

		// @optional -(void)onSignInterpreterLanguageChanged;
		[Export ("onSignInterpreterLanguageChanged")]
		void OnSignInterpreterLanguageChanged ();

		// @optional -(void)onAvailableSignLanguageListUpdated:(NSArray<MobileRTCSignInterpreterLanguage *> * _Nullable)availableSignLanguageList;
		[Export ("onAvailableSignLanguageListUpdated:")]
		void OnAvailableSignLanguageListUpdated ([NullAllowed] MobileRTCSignInterpreterLanguage[] availableSignLanguageList);

		// @optional -(void)onRequestSignInterpreterToTalk;
		[Export ("onRequestSignInterpreterToTalk")]
		void OnRequestSignInterpreterToTalk ();

		// @optional -(void)onTalkPrivilegeChanged:(BOOL)hasPrivilege;
		[Export ("onTalkPrivilegeChanged:")]
		void OnTalkPrivilegeChanged (bool hasPrivilege);
	}

	// @protocol MobileRTCWebinarServiceDelegate <MobileRTCMeetingServiceDelegate>
	[Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MobileRTCWebinarServiceDelegate : IMobileRTCMeetingServiceDelegate
	{
		// @optional -(void)onSinkQAConnectStarted;
		[Export ("onSinkQAConnectStarted")]
		void OnSinkQAConnectStarted ();

		// @optional -(void)onSinkQAConnected:(BOOL)connected;
		[Export ("onSinkQAConnected:")]
		void OnSinkQAConnected (bool connected);

		// @optional -(void)onSinkQAOpenQuestionChanged:(NSInteger)count;
		[Export ("onSinkQAOpenQuestionChanged:")]
		void OnSinkQAOpenQuestionChanged (nint count);

		// @optional -(void)onSinkQAAddQuestion:(NSString * _Nonnull)questionID success:(BOOL)success;
		[Export ("onSinkQAAddQuestion:success:")]
		void OnSinkQAAddQuestion (string questionID, bool success);

		// @optional -(void)onSinkQAAddAnswer:(NSString * _Nonnull)answerID success:(BOOL)success;
		[Export ("onSinkQAAddAnswer:success:")]
		void OnSinkQAAddAnswer (string answerID, bool success);

		// @optional -(void)onSinkQuestionMarkedAsDismissed:(NSString * _Nonnull)questionID;
		[Export ("onSinkQuestionMarkedAsDismissed:")]
		void OnSinkQuestionMarkedAsDismissed (string questionID);

		// @optional -(void)onSinkReopenQuestion:(NSString * _Nonnull)questionID;
		[Export ("onSinkReopenQuestion:")]
		void OnSinkReopenQuestion (string questionID);

		// @optional -(void)onSinkReceiveQuestion:(NSString * _Nonnull)questionID;
		[Export ("onSinkReceiveQuestion:")]
		void OnSinkReceiveQuestion (string questionID);

		// @optional -(void)onSinkReceiveAnswer:(NSString * _Nonnull)answerID;
		[Export ("onSinkReceiveAnswer:")]
		void OnSinkReceiveAnswer (string answerID);

		// @optional -(void)onSinkUserLivingReply:(NSString * _Nonnull)questionID;
		[Export ("onSinkUserLivingReply:")]
		void OnSinkUserLivingReply (string questionID);

		// @optional -(void)onSinkUserEndLiving:(NSString * _Nonnull)questionID;
		[Export ("onSinkUserEndLiving:")]
		void OnSinkUserEndLiving (string questionID);

		// @optional -(void)onSinkVoteupQuestion:(NSString * _Nonnull)questionID orderChanged:(BOOL)orderChanged;
		[Export ("onSinkVoteupQuestion:orderChanged:")]
		void OnSinkVoteupQuestion (string questionID, bool orderChanged);

		// @optional -(void)onSinkRevokeVoteupQuestion:(NSString * _Nonnull)questionID orderChanged:(BOOL)orderChanged;
		[Export ("onSinkRevokeVoteupQuestion:orderChanged:")]
		void OnSinkRevokeVoteupQuestion (string questionID, bool orderChanged);

		// @optional -(void)onSinkDeleteQuestion:(NSArray<NSString *> * _Nonnull)questionIDArray;
		[Export ("onSinkDeleteQuestion:")]
		void OnSinkDeleteQuestion (string[] questionIDArray);

		// @optional -(void)onSinkDeleteAnswer:(NSArray<NSString *> * _Nonnull)answerIDArray;
		[Export ("onSinkDeleteAnswer:")]
		void OnSinkDeleteAnswer (string[] answerIDArray);

		// @optional -(void)onSinkQAAllowAskQuestionAnonymouslyNotification:(BOOL)beAllowed;
		[Export ("onSinkQAAllowAskQuestionAnonymouslyNotification:")]
		void OnSinkQAAllowAskQuestionAnonymouslyNotification (bool beAllowed);

		// @optional -(void)onSinkQAAllowAttendeeViewAllQuestionNotification:(BOOL)beAllowed;
		[Export ("onSinkQAAllowAttendeeViewAllQuestionNotification:")]
		void OnSinkQAAllowAttendeeViewAllQuestionNotification (bool beAllowed);

		// @optional -(void)onSinkQAAllowAttendeeUpVoteQuestionNotification:(BOOL)beAllowed;
		[Export ("onSinkQAAllowAttendeeUpVoteQuestionNotification:")]
		void OnSinkQAAllowAttendeeUpVoteQuestionNotification (bool beAllowed);

		// @optional -(void)onSinkQAAllowAttendeeAnswerQuestionNotification:(BOOL)beAllowed;
		[Export ("onSinkQAAllowAttendeeAnswerQuestionNotification:")]
		void OnSinkQAAllowAttendeeAnswerQuestionNotification (bool beAllowed);

		// @optional -(void)onSinkWebinarNeedRegister:(NSString * _Nonnull)registerURL;
		[Export ("onSinkWebinarNeedRegister:")]
		void OnSinkWebinarNeedRegister (string registerURL);

		// @optional -(void)onSinkJoinWebinarNeedUserNameAndEmailWithCompletion:(BOOL (^ _Nonnull)(NSString * _Nonnull, NSString * _Nonnull, BOOL))completion;
		[Export ("onSinkJoinWebinarNeedUserNameAndEmailWithCompletion:")]
		void OnSinkJoinWebinarNeedUserNameAndEmailWithCompletion (Func<NSString, NSString, bool, bool> completion);

		// @optional -(void)onSinkPanelistCapacityExceed;
		[Export ("onSinkPanelistCapacityExceed")]
		void OnSinkPanelistCapacityExceed ();

		// @optional -(void)onSinkPromptAttendee2PanelistResult:(MobileRTCWebinarPromoteorDepromoteError)errorCode;
		[Export ("onSinkPromptAttendee2PanelistResult:")]
		void OnSinkPromptAttendee2PanelistResult (MobileRTCWebinarPromoteorDepromoteError errorCode);

		// @optional -(void)onSinkDePromptPanelist2AttendeeResult:(MobileRTCWebinarPromoteorDepromoteError)errorCode;
		[Export ("onSinkDePromptPanelist2AttendeeResult:")]
		void OnSinkDePromptPanelist2AttendeeResult (MobileRTCWebinarPromoteorDepromoteError errorCode);

		// @optional -(void)onSinkAllowAttendeeChatNotification:(MobileRTCChatAllowAttendeeChat)currentPrivilege;
		[Export ("onSinkAllowAttendeeChatNotification:")]
		void OnSinkAllowAttendeeChatNotification (MobileRTCChatAllowAttendeeChat currentPrivilege);

		// @optional -(void)onSinkAttendeePromoteConfirmResult:(BOOL)agree userId:(NSUInteger)userId;
		[Export ("onSinkAttendeePromoteConfirmResult:userId:")]
		void OnSinkAttendeePromoteConfirmResult (bool agree, nuint userId);

		// @optional -(void)onSinkSelfAllowTalkNotification;
		[Export ("onSinkSelfAllowTalkNotification")]
		void OnSinkSelfAllowTalkNotification ();

		// @optional -(void)onSinkSelfDisallowTalkNotification;
		[Export ("onSinkSelfDisallowTalkNotification")]
		void OnSinkSelfDisallowTalkNotification ();

		// @optional -(void)onAllowWebinarReactionStatusChanged:(BOOL)canReaction;
		[Export ("onAllowWebinarReactionStatusChanged:")]
		void OnAllowWebinarReactionStatusChanged (bool canReaction);

		// @optional -(void)onAllowAttendeeRaiseHandStatusChanged:(BOOL)canRaiseHand;
		[Export ("onAllowAttendeeRaiseHandStatusChanged:")]
		void OnAllowAttendeeRaiseHandStatusChanged (bool canRaiseHand);

		// @optional -(void)onAllowAttendeeViewTheParticipantCountStatusChanged:(BOOL)canViewParticipantCount;
		[Export ("onAllowAttendeeViewTheParticipantCountStatusChanged:")]
		void OnAllowAttendeeViewTheParticipantCountStatusChanged (bool canViewParticipantCount);
	}

	// @protocol MobileRTCLiveTranscriptionServiceDelegate <MobileRTCMeetingServiceDelegate>
	[Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MobileRTCLiveTranscriptionServiceDelegate : IMobileRTCMeetingServiceDelegate
	{
		// @optional -(void)onCaptionStatusChanged:(BOOL)enable;
		[Export ("onCaptionStatusChanged:")]
		void OnCaptionStatusChanged (bool enable);

		// @optional -(void)onSinkLiveTranscriptionStatus:(MobileRTCLiveTranscriptionStatus)status;
		[Export ("onSinkLiveTranscriptionStatus:")]
		void OnSinkLiveTranscriptionStatus (MobileRTCLiveTranscriptionStatus status);

		// @optional -(void)onSinkLiveTranscriptionMsgReceived:(NSString * _Nonnull)msg speakerId:(NSUInteger)speakerId type:(MobileRTCLiveTranscriptionOperationType)type __attribute__((deprecated("Use -onLiveTranscriptionMsgInfoReceived: instead")));
		[Export ("onSinkLiveTranscriptionMsgReceived:speakerId:type:")]
		void OnSinkLiveTranscriptionMsgReceived (string msg, nuint speakerId, MobileRTCLiveTranscriptionOperationType type);

		// @optional -(void)onLiveTranscriptionMsgInfoReceived:(MobileRTCLiveTranscriptionMessageInfo * _Nullable)messageInfo;
		[Export ("onLiveTranscriptionMsgInfoReceived:")]
		void OnLiveTranscriptionMsgInfoReceived ([NullAllowed] MobileRTCLiveTranscriptionMessageInfo messageInfo);

		// @optional -(void)onOriginalLanguageMsgReceived:(MobileRTCLiveTranscriptionMessageInfo * _Nullable)messageInfo;
		[Export ("onOriginalLanguageMsgReceived:")]
		void OnOriginalLanguageMsgReceived ([NullAllowed] MobileRTCLiveTranscriptionMessageInfo messageInfo);

		// @optional -(void)onLiveTranscriptionMsgError:(MobileRTCLiveTranscriptionLanguage * _Nullable)speakLanguage transcriptLanguage:(MobileRTCLiveTranscriptionLanguage * _Nullable)transcriptLanguage;
		[Export ("onLiveTranscriptionMsgError:transcriptLanguage:")]
		void OnLiveTranscriptionMsgError ([NullAllowed] MobileRTCLiveTranscriptionLanguage speakLanguage, [NullAllowed] MobileRTCLiveTranscriptionLanguage transcriptLanguage);

		// @optional -(void)onSinkRequestForLiveTranscriptReceived:(NSUInteger)requesterUserId bAnonymous:(BOOL)bAnonymous;
		[Export ("onSinkRequestForLiveTranscriptReceived:bAnonymous:")]
		void OnSinkRequestForLiveTranscriptReceived (nuint requesterUserId, bool bAnonymous);
	}

	// @protocol MobileRTC3DAvatarDelegate <MobileRTCMeetingServiceDelegate>
	[Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MobileRTC3DAvatarDelegate : IMobileRTCMeetingServiceDelegate
	{
		// @optional -(void)on3DAvatarItemThumbnailsDownloaded;
		[Export ("on3DAvatarItemThumbnailsDownloaded")]
		void On3DAvatarItemThumbnailsDownloaded ();

		// @optional -(void)on3DAvatarItemDataDownloading:(int)index;
		[Export ("on3DAvatarItemDataDownloading:")]
		void On3DAvatarItemDataDownloading (int index);

		// @optional -(void)on3DAvatarItemDataDownloaded:(_Bool)success andIndex:(int)index;
		[Export ("on3DAvatarItemDataDownloaded:andIndex:")]
		void On3DAvatarItemDataDownloaded (bool success, int index);
	}

	// @protocol MobileRTCCustomizedUIMeetingDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface MobileRTCCustomizedUIMeetingDelegate
	{
		// @required -(void)onInitMeetingView;
		[Abstract]
		[Export ("onInitMeetingView")]
		void OnInitMeetingView ();

		// @required -(void)onDestroyMeetingView;
		[Abstract]
		[Export ("onDestroyMeetingView")]
		void OnDestroyMeetingView ();
	}

	// @protocol MobileRTCVideoRawDataDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface MobileRTCVideoRawDataDelegate
	{
		// @optional -(void)onMobileRTCRender:(MobileRTCRenderer * _Nonnull)renderer framePixelBuffer:(CVPixelBufferRef _Nullable)pixelBuffer rotation:(MobileRTCVideoRawDataRotation)rotation;
		[Export ("onMobileRTCRender:framePixelBuffer:rotation:")]
		void FramePixelBuffer (MobileRTCRenderer renderer, [NullAllowed] CVPixelBuffer pixelBuffer, MobileRTCVideoRawDataRotation rotation);

		// @optional -(void)onMobileRTCRender:(MobileRTCRenderer * _Nonnull)renderer frameRawData:(MobileRTCVideoRawData * _Nonnull)rawData;
		[Export ("onMobileRTCRender:frameRawData:")]
		void FrameRawData (MobileRTCRenderer renderer, MobileRTCVideoRawData rawData);

		// @optional -(void)onMobileRTCRender:(MobileRTCRenderer * _Nonnull)renderer rawDataSending:(BOOL)on;
		[Export ("onMobileRTCRender:rawDataSending:")]
		void RawDataSending (MobileRTCRenderer renderer, bool on);
	}

	// @protocol MobileRTCAudioRawDataDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface MobileRTCAudioRawDataDelegate
	{
		// @optional -(void)onMobileRTCMixedAudioRawData:(MobileRTCAudioRawData * _Nonnull)rawData;
		[Export ("onMobileRTCMixedAudioRawData:")]
		void OnMobileRTCMixedAudioRawData (MobileRTCAudioRawData rawData);

		// @optional -(void)onMobileRTCOneWayAudioAudioRawData:(MobileRTCAudioRawData * _Nonnull)rawData userId:(NSUInteger)userId;
		[Export ("onMobileRTCOneWayAudioAudioRawData:userId:")]
		void OnMobileRTCOneWayAudioAudioRawData (MobileRTCAudioRawData rawData, nuint userId);
	}

	// @protocol MobileRTCAudioSourceDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface MobileRTCAudioSourceDelegate
	{
		// @optional -(void)onDeviceInitialize:(MobileRTCAudioSender * _Nonnull)rawdataSender;
		[Export ("onDeviceInitialize:")]
		void OnDeviceInitialize (MobileRTCAudioSender rawdataSender);

		// @optional -(void)onStartSendData;
		[Export ("onStartSendData")]
		void OnStartSendData ();

		// @optional -(void)onStopSendData;
		[Export ("onStopSendData")]
		void OnStopSendData ();

		// @optional -(void)onDeviceUninitialize;
		[Export ("onDeviceUninitialize")]
		void OnDeviceUninitialize ();
	}

	// @protocol MobileRTCPreProcessorDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface MobileRTCPreProcessorDelegate
	{
		// @optional -(void)onPreProcessRawData:(MobileRTCPreProcessRawData * _Nonnull)rawData;
		[Export ("onPreProcessRawData:")]
		void OnPreProcessRawData (MobileRTCPreProcessRawData rawData);
	}

	// @protocol MobileRTCVideoSourceDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface MobileRTCVideoSourceDelegate
	{
		// @optional -(void)onInitialize:(MobileRTCVideoSender * _Nonnull)rawDataSender supportCapabilityArray:(NSArray<MobileRTCVideoCapabilityItem *> * _Nonnull)supportCapabilityArray suggestCapabilityItem:(MobileRTCVideoCapabilityItem * _Nonnull)suggestCapabilityItem;
		[Export ("onInitialize:supportCapabilityArray:suggestCapabilityItem:")]
		void OnInitialize (MobileRTCVideoSender rawDataSender, MobileRTCVideoCapabilityItem[] supportCapabilityArray, MobileRTCVideoCapabilityItem suggestCapabilityItem);

		// @optional -(void)onPropertyChange:(NSArray<MobileRTCVideoCapabilityItem *> * _Nonnull)supportCapabilityArray suggestCapabilityItem:(MobileRTCVideoCapabilityItem * _Nonnull)suggestCapabilityItem;
		[Export ("onPropertyChange:suggestCapabilityItem:")]
		void OnPropertyChange (MobileRTCVideoCapabilityItem[] supportCapabilityArray, MobileRTCVideoCapabilityItem suggestCapabilityItem);

		// @optional -(void)onStartSend;
		[Export ("onStartSend")]
		void OnStartSend ();

		// @optional -(void)onStopSend;
		[Export ("onStopSend")]
		void OnStopSend ();

		// @optional -(void)onUninitialized;
		[Export ("onUninitialized")]
		void OnUninitialized ();
	}

	// @protocol MobileRTCShareSourceDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface MobileRTCShareSourceDelegate
	{
		// @optional -(void)onStartSend:(MobileRTCShareSender * _Nonnull)sender;
		[Export ("onStartSend:")]
		void OnStartSend (MobileRTCShareSender sender);

		// @optional -(void)onStopSend;
		[Export ("onStopSend")]
		void OnStopSend ();
	}

	// @protocol MobileRTCShareAudioSourceDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface MobileRTCShareAudioSourceDelegate
	{
		// @optional -(void)onStartSendAudio:(MobileRTCShareAudioSender * _Nonnull)sender;
		[Export ("onStartSendAudio:")]
		void OnStartSendAudio (MobileRTCShareAudioSender sender);

		// @optional -(void)onStopSendAudio;
		[Export ("onStopSendAudio")]
		void OnStopSendAudio ();
	}

	// @protocol MobileRTCSMSServiceDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface MobileRTCSMSServiceDelegate
	{
		// @optional -(void)onNeedRealNameAuth:(NSArray<MobileRTCRealNameCountryInfo *> * _Nonnull)supportCountryList privacyURL:(NSString * _Nonnull)privacyUrl retrieveHandle:(MobileRTCRetrieveSMSHandler * _Nonnull)handle;
		[Export ("onNeedRealNameAuth:privacyURL:retrieveHandle:")]
		void OnNeedRealNameAuth (MobileRTCRealNameCountryInfo[] supportCountryList, string privacyUrl, MobileRTCRetrieveSMSHandler handle);

		// @optional -(void)onRetrieveSMSVerificationCodeResultNotification:(MobileRTCSMSRetrieveResult)result verifyHandle:(MobileRTCVerifySMSHandler * _Nonnull)handler;
		[Export ("onRetrieveSMSVerificationCodeResultNotification:verifyHandle:")]
		void OnRetrieveSMSVerificationCodeResultNotification (MobileRTCSMSRetrieveResult result, MobileRTCVerifySMSHandler handler);

		// @optional -(void)onVerifySMSVerificationCodeResultNotification:(MobileRTCSMSVerifyResult)result;
		[Export ("onVerifySMSVerificationCodeResultNotification:")]
		void OnVerifySMSVerificationCodeResultNotification (MobileRTCSMSVerifyResult result);
	}

	// @protocol MobileRTCBOServiceDelegate <MobileRTCMeetingServiceDelegate>
	[Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MobileRTCBOServiceDelegate : IMobileRTCMeetingServiceDelegate
	{
		// @optional -(void)onHasCreatorRightsNotification:(MobileRTCBOCreator * _Nonnull)creator;
		[Export ("onHasCreatorRightsNotification:")]
		void OnHasCreatorRightsNotification (MobileRTCBOCreator creator);

		// @optional -(void)onHasAdminRightsNotification:(MobileRTCBOAdmin * _Nonnull)admin;
		[Export ("onHasAdminRightsNotification:")]
		void OnHasAdminRightsNotification (MobileRTCBOAdmin admin);

		// @optional -(void)onHasAssistantRightsNotification:(MobileRTCBOAssistant * _Nonnull)assistant;
		[Export ("onHasAssistantRightsNotification:")]
		void OnHasAssistantRightsNotification (MobileRTCBOAssistant assistant);

		// @optional -(void)onHasAttendeeRightsNotification:(MobileRTCBOAttendee * _Nonnull)attendee;
		[Export ("onHasAttendeeRightsNotification:")]
		void OnHasAttendeeRightsNotification (MobileRTCBOAttendee attendee);

		// @optional -(void)onHasDataHelperRightsNotification:(MobileRTCBOData * _Nonnull)dataHelper;
		[Export ("onHasDataHelperRightsNotification:")]
		void OnHasDataHelperRightsNotification (MobileRTCBOData dataHelper);

		// @optional -(void)onBroadcastBOVoiceStatus:(BOOL)bStart;
		[Export ("onBroadcastBOVoiceStatus:")]
		void OnBroadcastBOVoiceStatus (bool bStart);

		// @optional -(void)onLostCreatorRightsNotification;
		[Export ("onLostCreatorRightsNotification")]
		void OnLostCreatorRightsNotification ();

		// @optional -(void)onLostAdminRightsNotification;
		[Export ("onLostAdminRightsNotification")]
		void OnLostAdminRightsNotification ();

		// @optional -(void)onLostAssistantRightsNotification;
		[Export ("onLostAssistantRightsNotification")]
		void OnLostAssistantRightsNotification ();

		// @optional -(void)onLostAttendeeRightsNotification;
		[Export ("onLostAttendeeRightsNotification")]
		void OnLostAttendeeRightsNotification ();

		// @optional -(void)onLostDataHelperRightsNotification;
		[Export ("onLostDataHelperRightsNotification")]
		void OnLostDataHelperRightsNotification ();

		// @optional -(void)onNewBroadcastMessageReceived:(NSString * _Nullable)broadcastMsg senderID:(NSUInteger)senderID;
		[Export ("onNewBroadcastMessageReceived:senderID:")]
		void OnNewBroadcastMessageReceived ([NullAllowed] string broadcastMsg, nuint senderID);

		// @optional -(void)onBOStopCountDown:(NSUInteger)seconds;
		[Export ("onBOStopCountDown:")]
		void OnBOStopCountDown (nuint seconds);

		// @optional -(void)onHostInviteReturnToMainSession:(NSString * _Nullable)hostName replyHandler:(MobileRTCReturnToMainSessionHandler * _Nullable)replyHandler;
		[Export ("onHostInviteReturnToMainSession:replyHandler:")]
		void OnHostInviteReturnToMainSession ([NullAllowed] string hostName, [NullAllowed] MobileRTCReturnToMainSessionHandler replyHandler);

		// @optional -(void)onBOStatusChanged:(MobileRTCBOStatus)status;
		[Export ("onBOStatusChanged:")]
		void OnBOStatusChanged (MobileRTCBOStatus status);

		// @optional -(void)onBOSwitchRequestReceived:(NSString * _Nullable)newBOName newBOID:(NSString * _Nullable)newBOID;
		[Export ("onBOSwitchRequestReceived:newBOID:")]
		void OnBOSwitchRequestReceived ([NullAllowed] string newBOName, [NullAllowed] string newBOID);
	}

	// @protocol MobileRTCReactionServiceDelegate <MobileRTCMeetingServiceDelegate>
	[Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MobileRTCReactionServiceDelegate : IMobileRTCMeetingServiceDelegate
	{
		// @optional -(void)onEmojiReactionReceived:(NSUInteger)userId reactionType:(MobileRTCEmojiReactionType)type reactionSkinTone:(MobileRTCEmojiReactionSkinTone)skinTone;
		[Export ("onEmojiReactionReceived:reactionType:reactionSkinTone:")]
		void OnEmojiReactionReceived (nuint userId, MobileRTCEmojiReactionType type, MobileRTCEmojiReactionSkinTone skinTone);

		// @optional -(void)onEmojiReactionReceivedInWebinar:(MobileRTCEmojiReactionType)type;
		[Export ("onEmojiReactionReceivedInWebinar:")]
		void OnEmojiReactionReceivedInWebinar (MobileRTCEmojiReactionType type);

		// @optional -(void)onEmojiFeedbackReceived:(NSUInteger)userId feedbackType:(MobileRTCEmojiFeedbackType)type;
		[Export ("onEmojiFeedbackReceived:feedbackType:")]
		void OnEmojiFeedbackReceived (nuint userId, MobileRTCEmojiFeedbackType type);

		// @optional -(void)onEmojiFeedbackCanceled:(NSUInteger)userId;
		[Export ("onEmojiFeedbackCanceled:")]
		void OnEmojiFeedbackCanceled (nuint userId);
	}

	// @protocol MobileRTCBODataDelegate <MobileRTCMeetingServiceDelegate>
	[Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MobileRTCBODataDelegate : IMobileRTCMeetingServiceDelegate
	{
		// @optional -(void)onBOInfoUpdated:(NSString * _Nullable)boId;
		[Export ("onBOInfoUpdated:")]
		void OnBOInfoUpdated ([NullAllowed] string boId);

		// @optional -(void)onUnAssignedUserUpdated;
		[Export ("onUnAssignedUserUpdated")]
		void OnUnAssignedUserUpdated ();

		// @optional -(void)onBOListInfoUpdated;
		[Export ("onBOListInfoUpdated")]
		void OnBOListInfoUpdated ();
	}

	// @protocol MobileRTCBOAdminDelegate <MobileRTCMeetingServiceDelegate>
	[Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MobileRTCBOAdminDelegate : IMobileRTCMeetingServiceDelegate
	{
		// @optional -(void)onHelpRequestReceived:(NSString * _Nullable)strUserID;
		[Export ("onHelpRequestReceived:")]
		void OnHelpRequestReceived ([NullAllowed] string strUserID);

		// @optional -(void)onStartBOError:(MobileRTCBOControllerError)errType;
		[Export ("onStartBOError:")]
		void OnStartBOError (MobileRTCBOControllerError errType);

		// @optional -(void)onBOEndTimerUpdated:(NSUInteger)remaining isTimesUpNotice:(BOOL)isTimesUpNotice;
		[Export ("onBOEndTimerUpdated:isTimesUpNotice:")]
		void OnBOEndTimerUpdated (nuint remaining, bool isTimesUpNotice);
	}

	// @protocol MobileRTCBOAttendeeDelegate <MobileRTCMeetingServiceDelegate>
	[Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MobileRTCBOAttendeeDelegate : IMobileRTCMeetingServiceDelegate
	{
		// @optional -(void)onHelpRequestHandleResultReceived:(MobileRTCBOHelpReply)eResult;
		[Export ("onHelpRequestHandleResultReceived:")]
		void OnHelpRequestHandleResultReceived (MobileRTCBOHelpReply eResult);

		// @optional -(void)onHostJoinedThisBOMeeting;
		[Export ("onHostJoinedThisBOMeeting")]
		void OnHostJoinedThisBOMeeting ();

		// @optional -(void)onHostLeaveThisBOMeeting;
		[Export ("onHostLeaveThisBOMeeting")]
		void OnHostLeaveThisBOMeeting ();
	}

	// @protocol MobileRTCBOCreatorDelegate <MobileRTCMeetingServiceDelegate>
	[Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MobileRTCBOCreatorDelegate : IMobileRTCMeetingServiceDelegate
	{
		// @optional -(void)onBOCreateSuccess:(NSString * _Nullable)BOID;
		[Export ("onBOCreateSuccess:")]
		void OnBOCreateSuccess ([NullAllowed] string BOID);

		// @optional -(void)onWebPreAssignBODataDownloadStatusChanged:(MobileRTCBOPreAssignBODataStatus)status;
		[Export ("onWebPreAssignBODataDownloadStatusChanged:")]
		void OnWebPreAssignBODataDownloadStatusChanged (MobileRTCBOPreAssignBODataStatus status);
	}

	// @interface MobileRTCMeetingStartParam : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCMeetingStartParam
	{
		// @property (assign, readwrite, nonatomic) BOOL isAppShare;
		[Export ("isAppShare")]
		bool IsAppShare { get; set; }

		// @property (assign, readwrite, nonatomic) BOOL noAudio;
		[Export ("noAudio")]
		bool NoAudio { get; set; }

		// @property (assign, readwrite, nonatomic) BOOL noVideo;
		[Export ("noVideo")]
		bool NoVideo { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable customerKey;
		[NullAllowed, Export ("customerKey", ArgumentSemantic.Retain)]
		string CustomerKey { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable vanityID;
		[NullAllowed, Export ("vanityID", ArgumentSemantic.Retain)]
		string VanityID { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable meetingNumber;
		[NullAllowed, Export ("meetingNumber", ArgumentSemantic.Retain)]
		string MeetingNumber { get; set; }

		// @property (assign, readwrite, nonatomic) BOOL isMyVoiceInMix;
		[Export ("isMyVoiceInMix")]
		bool IsMyVoiceInMix { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable inviteContactID;
		[NullAllowed, Export ("inviteContactID")]
		string InviteContactID { get; set; }
	}

	// @interface MobileRTCMeetingStartParam4LoginlUser : MobileRTCMeetingStartParam
	[BaseType (typeof(MobileRTCMeetingStartParam))]
	interface MobileRTCMeetingStartParam4LoginlUser
	{
	}

	// @interface MobileRTCMeetingStartParam4WithoutLoginUser : MobileRTCMeetingStartParam
	[BaseType (typeof(MobileRTCMeetingStartParam))]
	interface MobileRTCMeetingStartParam4WithoutLoginUser
	{
		// @property (assign, readwrite, nonatomic) MobileRTCUserType userType;
		[Export ("userType", ArgumentSemantic.Assign)]
		MobileRTCUserType UserType { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable userName;
		[NullAllowed, Export ("userName", ArgumentSemantic.Retain)]
		string UserName { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nonnull zak;
		[Export ("zak", ArgumentSemantic.Retain)]
		string Zak { get; set; }
	}

	// @interface MobileRTCMeetingJoinParam : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCMeetingJoinParam
	{
		// @property (assign, readwrite, nonatomic) BOOL noAudio;
		[Export ("noAudio")]
		bool NoAudio { get; set; }

		// @property (assign, readwrite, nonatomic) BOOL noVideo;
		[Export ("noVideo")]
		bool NoVideo { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable customerKey;
		[NullAllowed, Export ("customerKey", ArgumentSemantic.Retain)]
		string CustomerKey { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable vanityID;
		[NullAllowed, Export ("vanityID", ArgumentSemantic.Retain)]
		string VanityID { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable meetingNumber;
		[NullAllowed, Export ("meetingNumber", ArgumentSemantic.Retain)]
		string MeetingNumber { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable userName;
		[NullAllowed, Export ("userName", ArgumentSemantic.Retain)]
		string UserName { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable password;
		[NullAllowed, Export ("password", ArgumentSemantic.Retain)]
		string Password { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable webinarToken;
		[NullAllowed, Export ("webinarToken", ArgumentSemantic.Retain)]
		string WebinarToken { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable zak;
		[NullAllowed, Export ("zak", ArgumentSemantic.Retain)]
		string Zak { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable appPrivilegeToken;
		[NullAllowed, Export ("appPrivilegeToken", ArgumentSemantic.Retain)]
		string AppPrivilegeToken { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable join_token;
		[NullAllowed, Export ("join_token", ArgumentSemantic.Retain)]
		string Join_token { get; set; }

		// @property (assign, readwrite, nonatomic) BOOL isMyVoiceInMix;
		[Export ("isMyVoiceInMix")]
		bool IsMyVoiceInMix { get; set; }
	}

	// @interface MobileRTCWebinarRegistLegalNoticeContent : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCWebinarRegistLegalNoticeContent
	{
		// @property (readwrite, retain, nonatomic) NSString * _Nullable formattedHtmlContent;
		[NullAllowed, Export ("formattedHtmlContent", ArgumentSemantic.Retain)]
		string FormattedHtmlContent { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable accountOwnerUrl;
		[NullAllowed, Export ("accountOwnerUrl", ArgumentSemantic.Retain)]
		string AccountOwnerUrl { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable termsUrl;
		[NullAllowed, Export ("termsUrl", ArgumentSemantic.Retain)]
		string TermsUrl { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable privacyPolicyUrl;
		[NullAllowed, Export ("privacyPolicyUrl", ArgumentSemantic.Retain)]
		string PrivacyPolicyUrl { get; set; }
	}

	// @interface MobileRTCMeetingParameter : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCMeetingParameter
	{
		// @property (assign, nonatomic) MobileRTCMeetingType meetingType;
		[Export ("meetingType", ArgumentSemantic.Assign)]
		MobileRTCMeetingType MeetingType { get; set; }

		// @property (assign, nonatomic) BOOL isViewOnly;
		[Export ("isViewOnly")]
		bool IsViewOnly { get; set; }

		// @property (assign, nonatomic) BOOL isAutoRecordingLocal;
		[Export ("isAutoRecordingLocal")]
		bool IsAutoRecordingLocal { get; set; }

		// @property (assign, nonatomic) BOOL isAutoRecordingCloud;
		[Export ("isAutoRecordingCloud")]
		bool IsAutoRecordingCloud { get; set; }

		// @property (assign, nonatomic) unsigned long long meetingNumber;
		[Export ("meetingNumber")]
		ulong MeetingNumber { get; set; }

		// @property (retain, nonatomic) NSString * _Nullable meetingTopic;
		[NullAllowed, Export ("meetingTopic", ArgumentSemantic.Retain)]
		string MeetingTopic { get; set; }

		// @property (retain, nonatomic) NSString * _Nullable meetingHost;
		[NullAllowed, Export ("meetingHost", ArgumentSemantic.Retain)]
		string MeetingHost { get; set; }
	}

	// @interface MobileRTCMeetingService : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCMeetingService
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		IMobileRTCMeetingServiceDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<MobileRTCMeetingServiceDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		[Wrap ("WeakCustomizedUImeetingDelegate")]
		[NullAllowed]
		MobileRTCCustomizedUIMeetingDelegate CustomizedUImeetingDelegate { get; set; }

		// @property (nonatomic, weak) id<MobileRTCCustomizedUIMeetingDelegate> _Nullable customizedUImeetingDelegate;
		[NullAllowed, Export ("customizedUImeetingDelegate", ArgumentSemantic.Weak)]
		NSObject WeakCustomizedUImeetingDelegate { get; set; }

		// -(MobileRTCMeetError)startMeetingWithStartParam:(MobileRTCMeetingStartParam * _Nonnull)param;
		[Export ("startMeetingWithStartParam:")]
		MobileRTCMeetError StartMeetingWithStartParam (MobileRTCMeetingStartParam param);

		// -(MobileRTCMeetError)joinMeetingWithJoinParam:(MobileRTCMeetingJoinParam * _Nonnull)param;
		[Export ("joinMeetingWithJoinParam:")]
		MobileRTCMeetError JoinMeetingWithJoinParam (MobileRTCMeetingJoinParam param);

		// -(MobileRTCMeetError)handZoomWebUrl:(NSString * _Nonnull)meetingUrl;
		[Export ("handZoomWebUrl:")]
		MobileRTCMeetError HandZoomWebUrl (string meetingUrl);

		// -(MobileRTCMeetingState)getMeetingState;
		[Export ("getMeetingState")]
		MobileRTCMeetingState MeetingState { get; }

		// -(void)leaveMeetingWithCmd:(LeaveMeetingCmd)cmd;
		[Export ("leaveMeetingWithCmd:")]
		void LeaveMeetingWithCmd (LeaveMeetingCmd cmd);

		// -(UIView * _Nullable)meetingView;
		[NullAllowed, Export ("meetingView")]
		UIView MeetingView { get; }

		// -(BOOL)setCustomizedInvitationDomain:(NSString * _Nonnull)invitationDomain;
		[Export ("setCustomizedInvitationDomain:")]
		bool SetCustomizedInvitationDomain (string invitationDomain);
	}

	// @interface MobileRTCAutoFramingParameter : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCAutoFramingParameter
	{
		// @property (assign, nonatomic) CGFloat ratio;
		[Export ("ratio")]
		nfloat Ratio { get; set; }

		// @property (assign, nonatomic) MobileRTCFaceRecognitionFailStrategy failStrategy;
		[Export ("failStrategy", ArgumentSemantic.Assign)]
		MobileRTCFaceRecognitionFailStrategy FailStrategy { get; set; }
	}

	// @interface AppShare (MobileRTCMeetingService)
	[Category]
	[BaseType (typeof(MobileRTCMeetingService))]
	interface MobileRTCMeetingService_AppShare
	{
		// -(BOOL)isDirectAppShareMeeting;
		[Export("isDirectAppShareMeeting")]
		bool IsDirectAppShareMeeting();

		// -(void)appShareWithView:(id _Nonnull)view;
		[Export ("appShareWithView:")]
		void AppShareWithView (NSObject view);

		// -(BOOL)startAppShare;
		[Export("startAppShare")]
		bool StartAppShare();

		// -(void)stopAppShare;
		[Export ("stopAppShare")]
		void StopAppShare ();

		// -(BOOL)isStartingShare;
		[Export("isStartingShare")]
		bool IsStartingShare();

		// -(BOOL)isViewingShare;
		[Export("isViewingShare")]
		bool IsViewingShare();

		// -(BOOL)isAnnotationOff;
		[Export("isAnnotationOff")]
		bool IsAnnotationOff();

		// -(BOOL)suspendSharing:(BOOL)suspend;
		[Export ("suspendSharing:")]
		bool SuspendSharing (bool suspend);

		// -(BOOL)isWhiteboardLegalNoticeAvailable;
		[Export("isWhiteboardLegalNoticeAvailable")]
		bool IsWhiteboardLegalNoticeAvailable();

		// -(NSString * _Nullable)getWhiteboardLegalNoticesPrompt;
		[Export("getWhiteboardLegalNoticesPrompt")]
		string WhiteboardLegalNoticesPrompt();

		// -(NSString * _Nullable)getWhiteboardLegalNoticesExplained;
		[Export("getWhiteboardLegalNoticesExplained")]
		string WhiteboardLegalNoticesExplained();

		// -(void)setShareAudio:(BOOL)enableAudio;
		[Export ("setShareAudio:")]
		void SetShareAudio (bool enableAudio);

		// -(BOOL)isSharedAudio;
		[Export("isSharedAudio")]
		bool IsSharedAudio();

		// -(BOOL)isDeviceSharing;
		[Export("isDeviceSharing")]
		bool IsDeviceSharing();
	}

	// @interface MobileRTCVideoStatus : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCVideoStatus
	{
		// @property (assign, nonatomic) BOOL isSending;
		[Export ("isSending")]
		bool IsSending { get; set; }

		// @property (assign, nonatomic) BOOL isReceiving;
		[Export ("isReceiving")]
		bool IsReceiving { get; set; }

		// @property (assign, nonatomic) BOOL isSource;
		[Export ("isSource")]
		bool IsSource { get; set; }
	}

	// @interface MobileRTCAudioStatus : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCAudioStatus
	{
		// @property (assign, nonatomic) BOOL isMuted;
		[Export ("isMuted")]
		bool IsMuted { get; set; }

		// @property (assign, nonatomic) BOOL isTalking;
		[Export ("isTalking")]
		bool IsTalking { get; set; }

		// @property (assign, nonatomic) MobileRTCAudioType audioType;
		[Export ("audioType", ArgumentSemantic.Assign)]
		MobileRTCAudioType AudioType { get; set; }
	}

	// @interface MobileRTCMeetingUserInfo : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCMeetingUserInfo
	{
		// @property (assign, nonatomic) NSUInteger userID;
		[Export ("userID")]
		nuint UserID { get; set; }

		// @property (retain, nonatomic) NSString * _Nullable persistentId;
		[NullAllowed, Export ("persistentId", ArgumentSemantic.Retain)]
		string PersistentId { get; set; }

		// @property (assign, nonatomic) BOOL isMySelf;
		[Export ("isMySelf")]
		bool IsMySelf { get; set; }

		// @property (retain, nonatomic) NSString * _Nullable customerKey;
		[NullAllowed, Export ("customerKey", ArgumentSemantic.Retain)]
		string CustomerKey { get; set; }

		// @property (retain, nonatomic) NSString * _Nonnull userName;
		[Export ("userName", ArgumentSemantic.Retain)]
		string UserName { get; set; }

		// @property (retain, nonatomic) NSString * _Nonnull avatarPath;
		[Export ("avatarPath", ArgumentSemantic.Retain)]
		string AvatarPath { get; set; }

		// @property (retain, nonatomic) MobileRTCVideoStatus * _Nonnull videoStatus;
		[Export ("videoStatus", ArgumentSemantic.Retain)]
		MobileRTCVideoStatus VideoStatus { get; set; }

		// @property (retain, nonatomic) MobileRTCAudioStatus * _Nonnull audioStatus;
		[Export ("audioStatus", ArgumentSemantic.Retain)]
		MobileRTCAudioStatus AudioStatus { get; set; }

		// @property (assign, nonatomic) BOOL handRaised;
		[Export ("handRaised")]
		bool HandRaised { get; set; }

		// @property (assign, nonatomic) BOOL inWaitingRoom;
		[Export ("inWaitingRoom")]
		bool InWaitingRoom { get; set; }

		// @property (assign, nonatomic) BOOL isCohost;
		[Export ("isCohost")]
		bool IsCohost { get; set; }

		// @property (assign, nonatomic) BOOL isHost;
		[Export ("isHost")]
		bool IsHost { get; set; }

		// @property (assign, nonatomic) BOOL isH323User;
		[Export ("isH323User")]
		bool IsH323User { get; set; }

		// @property (assign, nonatomic) BOOL isPureCallInUser;
		[Export ("isPureCallInUser")]
		bool IsPureCallInUser { get; set; }

		// @property (assign, nonatomic) BOOL isSharingPureComputerAudio;
		[Export ("isSharingPureComputerAudio")]
		bool IsSharingPureComputerAudio { get; set; }

		// @property (assign, nonatomic) MobileRTCFeedbackType feedbackType __attribute__((deprecated("Use emojiFeedbackType instead")));
		[Export ("feedbackType", ArgumentSemantic.Assign)]
		MobileRTCFeedbackType FeedbackType { get; set; }

		// @property (assign, nonatomic) MobileRTCEmojiFeedbackType emojiFeedbackType;
		[Export ("emojiFeedbackType", ArgumentSemantic.Assign)]
		MobileRTCEmojiFeedbackType EmojiFeedbackType { get; set; }

		// @property (assign, nonatomic) MobileRTCUserRole userRole;
		[Export ("userRole", ArgumentSemantic.Assign)]
		MobileRTCUserRole UserRole { get; set; }

		// @property (assign, nonatomic) BOOL isInterpreter;
		[Export ("isInterpreter")]
		bool IsInterpreter { get; set; }

		// @property (retain, nonatomic) NSString * _Nullable interpreterActiveLanguage;
		[NullAllowed, Export ("interpreterActiveLanguage", ArgumentSemantic.Retain)]
		string InterpreterActiveLanguage { get; set; }
	}

	// @interface MobileRTCMeetingWebinarAttendeeInfo : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCMeetingWebinarAttendeeInfo
	{
		// @property (assign, nonatomic) NSUInteger userID;
		[Export ("userID")]
		nuint UserID { get; set; }

		// @property (assign, nonatomic) BOOL isMySelf;
		[Export ("isMySelf")]
		bool IsMySelf { get; set; }

		// @property (retain, nonatomic) NSString * _Nullable userName;
		[NullAllowed, Export ("userName", ArgumentSemantic.Retain)]
		string UserName { get; set; }

		// @property (assign, nonatomic) MobileRTCUserRole userRole;
		[Export ("userRole", ArgumentSemantic.Assign)]
		MobileRTCUserRole UserRole { get; set; }

		// @property (assign, nonatomic) BOOL handRaised;
		[Export ("handRaised")]
		bool HandRaised { get; set; }

		// @property (assign, nonatomic) BOOL isAttendeeCanTalk;
		[Export ("isAttendeeCanTalk")]
		bool IsAttendeeCanTalk { get; set; }

		// @property (retain, nonatomic) MobileRTCAudioStatus * _Nonnull audioStatus;
		[Export ("audioStatus", ArgumentSemantic.Retain)]
		MobileRTCAudioStatus AudioStatus { get; set; }
	}

	// @interface MobileRTCMeetingChat : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCMeetingChat
	{
		// @property (readwrite, retain, nonatomic) NSString * _Nullable chatId;
		[NullAllowed, Export ("chatId", ArgumentSemantic.Retain)]
		string ChatId { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable senderId;
		[NullAllowed, Export ("senderId", ArgumentSemantic.Retain)]
		string SenderId { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable senderName;
		[NullAllowed, Export ("senderName", ArgumentSemantic.Retain)]
		string SenderName { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable receiverId;
		[NullAllowed, Export ("receiverId", ArgumentSemantic.Retain)]
		string ReceiverId { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable receiverName;
		[NullAllowed, Export ("receiverName", ArgumentSemantic.Retain)]
		string ReceiverName { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * _Nullable content;
		[NullAllowed, Export ("content", ArgumentSemantic.Retain)]
		string Content { get; set; }

		// @property (readwrite, retain, nonatomic) NSDate * _Nullable date;
		[NullAllowed, Export ("date", ArgumentSemantic.Retain)]
		NSDate Date { get; set; }

		// @property (readwrite, nonatomic) MobileRTCChatMessageType chatMessageType;
		[Export ("chatMessageType", ArgumentSemantic.Assign)]
		MobileRTCChatMessageType ChatMessageType { get; set; }

		// @property (readwrite, nonatomic) BOOL isMyself;
		[Export ("isMyself")]
		bool IsMyself { get; set; }

		// @property (readwrite, nonatomic) BOOL isPrivate;
		[Export ("isPrivate")]
		bool IsPrivate { get; set; }

		// @property (readwrite, nonatomic) BOOL isChatToAll;
		[Export ("isChatToAll")]
		bool IsChatToAll { get; set; }

		// @property (readwrite, nonatomic) BOOL isChatToAllPanelist;
		[Export ("isChatToAllPanelist")]
		bool IsChatToAllPanelist { get; set; }

		// @property (readwrite, nonatomic) BOOL isChatToWaitingroom;
		[Export ("isChatToWaitingroom")]
		bool IsChatToWaitingroom { get; set; }
	}

	// @interface MobileRTCRequestLocalRecordingPrivilegeHandler : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCRequestLocalRecordingPrivilegeHandler
	{
		// -(NSString * _Nullable)getRequestId;
		[NullAllowed, Export ("getRequestId")]
		string RequestId { get; }

		// -(NSInteger)getRequesterId;
		[Export ("getRequesterId")]
		nint RequesterId { get; }

		// -(NSString * _Nullable)getRequesterName;
		[NullAllowed, Export ("getRequesterName")]
		string RequesterName { get; }

		// -(MobileRTCSDKError)grantLocalRecordingPrivilege;
		[Export ("grantLocalRecordingPrivilege")]
		MobileRTCSDKError GrantLocalRecordingPrivilege { get; }

		// -(MobileRTCSDKError)denyLocalRecordingPrivilege;
		[Export ("denyLocalRecordingPrivilege")]
		MobileRTCSDKError DenyLocalRecordingPrivilege { get; }
	}

	// @interface InMeeting (MobileRTCMeetingService)
	[Category]
	[BaseType (typeof(MobileRTCMeetingService))]
	interface MobileRTCMeetingService_InMeeting
	{
		// -(BOOL)isMeetingHost;
		[Export("isMeetingHost")]
		bool IsMeetingHost();

		// -(BOOL)isMeetingCoHost;
		[Export("isMeetingCoHost")]
		bool IsMeetingCoHost();

		// -(BOOL)isWebinarAttendee;
		[Export("isWebinarAttendee")]
		bool IsWebinarAttendee();

		// -(BOOL)isWebinarPanelist;
		[Export ("isWebinarPanelist")]
		bool IsWebinarPanelist();

		// -(BOOL)isMeetingLocked;
		[Export("isMeetingLocked")]
		bool IsMeetingLocked();

		// -(BOOL)isShareLocked;
		[Export("isShareLocked")]
		bool IsShareLocked();

		// -(BOOL)isCMREnabled;
		[Export("isCMREnabled")]
		bool IsCMREnabled();

		// -(BOOL)isCMRInProgress;
		[Export("isCMRInProgress")]
		bool IsCMRInProgress();

		// -(BOOL)isCMRPaused;
		[Export("isCMRPaused")]
		bool IsCMRPaused();

		// -(BOOL)resumePauseCMR;
		[Export("resumePauseCMR")]
		bool ResumePauseCMR();

		// -(void)turnOnCMR:(BOOL)on;
		[Export ("turnOnCMR:")]
		void TurnOnCMR (bool on);

		// -(MobileRTCRecordingStatus)getCloudRecordingStatus;
		[Export("getCloudRecordingStatus")]
		MobileRTCRecordingStatus CloudRecordingStatus();

		// -(BOOL)isFailoverMeeting;
		[Export("isFailoverMeeting")]
		bool IsFailoverMeeting();

		// -(MobileRTCMeetingType)getMeetingType;
		[Export("getMeetingType")]
		MobileRTCMeetingType MeetingType();

		// -(BOOL)isWebinarMeeting;
		[Export("isWebinarMeeting")]
		bool IsWebinarMeeting();

		// -(BOOL)lockMeeting:(BOOL)lock;
		[Export ("lockMeeting:")]
		bool LockMeeting (bool @lock);

		// -(BOOL)lockShare:(BOOL)lock;
		[Export ("lockShare:")]
		bool LockShare (bool @lock);

		// -(MobileRTCNetworkQuality)queryNetworkQuality:(MobileRTCComponentType)type withDataFlow:(BOOL)sending;
		[Export ("queryNetworkQuality:withDataFlow:")]
		MobileRTCNetworkQuality QueryNetworkQuality (MobileRTCComponentType type, bool sending);

		// -(BOOL)presentMeetingChatViewController:(UIViewController * _Nonnull)parentVC userId:(NSInteger)userId;
		[Export ("presentMeetingChatViewController:userId:")]
		bool PresentMeetingChatViewController (UIViewController parentVC, nint userId);

		// -(BOOL)presentParticipantsViewController:(UIViewController * _Nonnull)parentVC;
		[Export ("presentParticipantsViewController:")]
		bool PresentParticipantsViewController (UIViewController parentVC);

		// -(BOOL)configDSCPWithAudioValue:(NSUInteger)audioValue VideoValue:(NSUInteger)videoValue;
		[Export ("configDSCPWithAudioValue:VideoValue:")]
		bool ConfigDSCPWithAudioValue (nuint audioValue, nuint videoValue);

		// -(BOOL)startLiveStreamWithStreamingURL:(NSString * _Nonnull)streamingURL StreamingKey:(NSString * _Nonnull)key BroadcastURL:(NSString * _Nonnull)broadcastURL;
		[Export ("startLiveStreamWithStreamingURL:StreamingKey:BroadcastURL:")]
		bool StartLiveStreamWithStreamingURL (string streamingURL, string key, string broadcastURL);

		// -(NSDictionary * _Nullable)getLiveStreamURL;
		[Export("getLiveStreamURL")]
		NSDictionary LiveStreamURL();

		// -(BOOL)stopLiveStream;
		[Export("stopLiveStream")]
		bool StopLiveStream();

		// -(BOOL)isRawLiveStreamSupported;
		[Export("isRawLiveStreamSupported")]
		bool IsRawLiveStreamSupported();

		// -(MobileRTCSDKError)canStartRawLiveStream;
		[Export("canStartRawLiveStream")]
		MobileRTCSDKError CanStartRawLiveStream();

		// -(MobileRTCSDKError)requestRawLiveStream:(NSString * _Nonnull)broadcastURL __attribute__((deprecated("Use -requestRawLiveStreaming: broadcastName: instead")));
		[Export ("requestRawLiveStream:")]
		MobileRTCSDKError RequestRawLiveStream (string broadcastURL);

		// -(MobileRTCSDKError)requestRawLiveStreaming:(NSString * _Nonnull)broadcastURL broadcastName:(NSString * _Nullable)broadcastName;
		[Export ("requestRawLiveStreaming:broadcastName:")]
		MobileRTCSDKError RequestRawLiveStreaming (string broadcastURL, [NullAllowed] string broadcastName);

		// -(MobileRTCSDKError)startRawLiveStream:(NSString * _Nonnull)broadcastURL __attribute__((deprecated("Use -startRawLiveStreaming: broadcastName: instead")));
		[Export ("startRawLiveStream:")]
		MobileRTCSDKError StartRawLiveStream (string broadcastURL);

		// -(MobileRTCSDKError)startRawLiveStreaming:(NSString * _Nonnull)broadcastURL broadcastName:(NSString * _Nullable)broadcastName;
		[Export ("startRawLiveStreaming:broadcastName:")]
		MobileRTCSDKError StartRawLiveStreaming (string broadcastURL, [NullAllowed] string broadcastName);

		// -(MobileRTCSDKError)stopRawLiveStream;
		[Export("stopRawLiveStream")]
		MobileRTCSDKError StopRawLiveStream();

		// -(MobileRTCSDKError)removeRawLiveStreamPrivilege:(NSUInteger)userId;
		[Export ("removeRawLiveStreamPrivilege:")]
		MobileRTCSDKError RemoveRawLiveStreamPrivilege (nuint userId);

		// -(NSArray<MobileRTCRawLiveStreamInfo *> * _Nullable)getRawLiveStreamingInfoList;
		[Export("getRawLiveStreamingInfoList")]
		MobileRTCRawLiveStreamInfo[] RawLiveStreamingInfoList();

		// -(NSArray<NSNumber *> * _Nullable)getRawLiveStreamPrivilegeUserList;
		[Export("getRawLiveStreamPrivilegeUserList")]
		NSNumber[] RawLiveStreamPrivilegeUserList();

		// -(BOOL)showMobileRTCMeeting:(void (^ _Nonnull)(void))completion;
		[Export ("showMobileRTCMeeting:")]
		bool ShowMobileRTCMeeting (Action completion);

		// -(BOOL)hideMobileRTCMeeting:(void (^ _Nonnull)(void))completion;
		[Export ("hideMobileRTCMeeting:")]
		bool HideMobileRTCMeeting (Action completion);

		// -(void)showMeetingControlBar;
		[Export ("showMeetingControlBar")]
		void ShowMeetingControlBar ();

		// -(void)switchToActiveSpeaker;
		[Export ("switchToActiveSpeaker")]
		void SwitchToActiveSpeaker ();

		// -(void)switchToVideoWall;
		[Export ("switchToVideoWall")]
		void SwitchToVideoWall ();

		// -(void)switchToDriveScene;
		[Export ("switchToDriveScene")]
		void SwitchToDriveScene ();

		// -(MobileRTCANNError)showAANPanelInView:(UIView * _Nullable)containerView originPoint:(CGPoint)originXY;
		[Export ("showAANPanelInView:originPoint:")]
		MobileRTCANNError ShowAANPanelInView ([NullAllowed] UIView containerView, CGPoint originXY);

		// -(MobileRTCANNError)hideAANPanel;
		[Export("hideAANPanel")]
		MobileRTCANNError HideAANPanel();

		// -(BOOL)isQAEnabled;
		[Export("isQAEnabled")]
		bool IsQAEnabled();

		// -(BOOL)presentQAViewController:(UIViewController * _Nonnull)parentVC;
		[Export ("presentQAViewController:")]
		bool PresentQAViewController (UIViewController parentVC);

		// -(NSString * _Nullable)getMeetingPassword;
		[Export("getMeetingPassword")]
		string MeetingPassword();

		// -(BOOL)showMinimizeMeetingFromZoomUIMeeting;
		[Export("showMinimizeMeetingFromZoomUIMeeting")]
		bool ShowMinimizeMeetingFromZoomUIMeeting();

		// -(BOOL)backZoomUIMeetingFromMinimizeMeeting;
		[Export("backZoomUIMeetingFromMinimizeMeeting")]
		bool BackZoomUIMeetingFromMinimizeMeeting();

		// -(BOOL)isParticipantsRenameAllowed;
		[Export("isParticipantsRenameAllowed")]
		bool IsParticipantsRenameAllowed();

		// -(void)allowParticipantsToRename:(BOOL)allow;
		[Export ("allowParticipantsToRename:")]
		void AllowParticipantsToRename (bool allow);

		// -(BOOL)isParticipantsUnmuteSelfAllowed;
		[Export("isParticipantsUnmuteSelfAllowed")]
		bool IsParticipantsUnmuteSelfAllowed();

		// -(void)allowParticipantsToUnmuteSelf:(BOOL)allow;
		[Export ("allowParticipantsToUnmuteSelf:")]
		void AllowParticipantsToUnmuteSelf (bool allow);

		// -(MobileRTCSDKError)allowParticipantsToStartVideo:(BOOL)allow;
		[Export ("allowParticipantsToStartVideo:")]
		MobileRTCSDKError AllowParticipantsToStartVideo (bool allow);

		// -(BOOL)isParticipantsStartVideoAllowed;
		[Export("isParticipantsStartVideoAllowed")]
		bool IsParticipantsStartVideoAllowed();

		// -(MobileRTCSDKError)allowParticipantsToShareWhiteBoard:(BOOL)allow;
		[Export ("allowParticipantsToShareWhiteBoard:")]
		MobileRTCSDKError AllowParticipantsToShareWhiteBoard (bool allow);

		// -(BOOL)isParticipantsShareWhiteBoardAllowed;
		[Export("isParticipantsShareWhiteBoardAllowed")]
		bool IsParticipantsShareWhiteBoardAllowed();

		// -(BOOL)isLiveTranscriptLegalNoticeAvailable;
		[Export("isLiveTranscriptLegalNoticeAvailable")]
		bool IsLiveTranscriptLegalNoticeAvailable();

		// -(NSString * _Nullable)getLiveTranscriptLegalNoticesPrompt;
		[Export("getLiveTranscriptLegalNoticesPrompt")]
		string LiveTranscriptLegalNoticesPrompt();

		// -(NSString * _Nullable)getLiveTranscriptLegalNoticesExplained;
		[Export("getLiveTranscriptLegalNoticesExplained")]
		string LiveTranscriptLegalNoticesExplained();

		// -(BOOL)isParticipantRequestLocalRecordingAllowed;
		[Export("isParticipantRequestLocalRecordingAllowed")]
		bool IsParticipantRequestLocalRecordingAllowed();

		// -(MobileRTCSDKError)allowParticipantsToRequestLocalRecording:(BOOL)allow;
		[Export ("allowParticipantsToRequestLocalRecording:")]
		MobileRTCSDKError AllowParticipantsToRequestLocalRecording (bool allow);

		// -(BOOL)isAutoAllowLocalRecordingRequest;
		[Export("isAutoAllowLocalRecordingRequest")]
		bool IsAutoAllowLocalRecordingRequest();

		// -(MobileRTCSDKError)autoAllowLocalRecordingRequest:(BOOL)allow;
		[Export ("autoAllowLocalRecordingRequest:")]
		MobileRTCSDKError AutoAllowLocalRecordingRequest (bool allow);

		// -(BOOL)canSuspendParticipantsActivities;
		[Export("canSuspendParticipantsActivities")]
		bool CanSuspendParticipantsActivities();

		// -(MobileRTCSDKError)suspendParticipantsActivites;
		[Export("suspendParticipantsActivites")]
		MobileRTCSDKError SuspendParticipantsActivites();
	}

	// @interface MobileRTCRoomDevice : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCRoomDevice
	{
		// @property (copy, nonatomic) NSString * deviceName;
		[Export ("deviceName")]
		string DeviceName { get; set; }

		// @property (copy, nonatomic) NSString * ipAddress;
		[Export ("ipAddress")]
		string IpAddress { get; set; }

		// @property (copy, nonatomic) NSString * e164num;
		[Export ("e164num")]
		string E164num { get; set; }

		// @property (assign, nonatomic) MobileRTCDeviceType deviceType;
		[Export ("deviceType", ArgumentSemantic.Assign)]
		MobileRTCDeviceType DeviceType { get; set; }

		// @property (assign, nonatomic) MobileRTCDeviceEncryptType encryptType;
		[Export ("encryptType", ArgumentSemantic.Assign)]
		MobileRTCDeviceEncryptType EncryptType { get; set; }
	}

	// @interface MobileRTCCallCountryCode : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCCallCountryCode
	{
		// @property (retain, nonatomic) NSString * countryId;
		[Export ("countryId", ArgumentSemantic.Retain)]
		string CountryId { get; set; }

		// @property (retain, nonatomic) NSString * countryName;
		[Export ("countryName", ArgumentSemantic.Retain)]
		string CountryName { get; set; }

		// @property (retain, nonatomic) NSString * countryCode;
		[Export ("countryCode", ArgumentSemantic.Retain)]
		string CountryCode { get; set; }

		// @property (retain, nonatomic) NSString * countryNumber;
		[Export ("countryNumber", ArgumentSemantic.Retain)]
		string CountryNumber { get; set; }

		// @property (assign, nonatomic) BOOL tollFree;
		[Export ("tollFree")]
		bool TollFree { get; set; }
	}

	// @interface Customize (MobileRTCMeetingService)
	[Category]
	[BaseType (typeof(MobileRTCMeetingService))]
	interface MobileRTCMeetingService_Customize
	{
		// -(void)customizeMeetingTitle:(NSString * _Nullable)title;
		[Export ("customizeMeetingTitle:")]
		void CustomizeMeetingTitle ([NullAllowed] string title);

		// -(BOOL)setMeetingTopic:(NSString * _Nonnull)meetingTopic;
		[Export ("setMeetingTopic:")]
		bool SetMeetingTopic (string meetingTopic);

		// -(BOOL)isCallRoomDeviceSupported;
		[Export("isCallRoomDeviceSupported")]
		bool IsCallRoomDeviceSupported();

		// -(BOOL)isCallingRoomDevice;
		[Export("isCallingRoomDevice")]
		bool IsCallingRoomDevice();

		// -(BOOL)cancelCallRoomDevice;
		[Export("cancelCallRoomDevice")]
		bool CancelCallRoomDevice();

		// -(NSArray<NSString *> * _Nullable)getIPAddressList;
		[Export("getIPAddressList")]
		string[] IPAddressList();

		// -(NSString * _Nullable)getH323MeetingPassword;
		[Export("getH323MeetingPassword")]
		string H323MeetingPassword();

		// -(NSArray<MobileRTCRoomDevice *> * _Nullable)getRoomDeviceList;
		[Export("getRoomDeviceList")]
		MobileRTCRoomDevice[] RoomDeviceList();

		// -(BOOL)sendPairingCode:(NSString * _Nonnull)code WithMeetingNumber:(unsigned long long)meetingNumber;
		[Export ("sendPairingCode:WithMeetingNumber:")]
		bool SendPairingCode (string code, ulong meetingNumber);

		// -(BOOL)callRoomDevice:(MobileRTCRoomDevice * _Nonnull)device;
		[Export ("callRoomDevice:")]
		bool CallRoomDevice (MobileRTCRoomDevice device);

		// -(NSUInteger)getParticipantID;
		[Export("getParticipantID")]
		nuint ParticipantID();

		// -(BOOL)setCustomizedPollingUrl:(NSString * _Nullable)pollingURL bCreate:(BOOL)bCreate;
		[Export ("setCustomizedPollingUrl:bCreate:")]
		bool SetCustomizedPollingUrl ([NullAllowed] string pollingURL, bool bCreate);

		// -(BOOL)setCloudWhiteboardFeedbackUrl:(NSString * _Nullable)feedbackUrl;
		[Export ("setCloudWhiteboardFeedbackUrl:")]
		bool SetCloudWhiteboardFeedbackUrl ([NullAllowed] string feedbackUrl);
	}

	// @interface Audio (MobileRTCMeetingService)
	[Category]
	[BaseType (typeof(MobileRTCMeetingService))]
	interface MobileRTCMeetingService_Audio
	{
		// -(MobileRTCAudioType)myAudioType;
		[Export("myAudioType")]
		MobileRTCAudioType MyAudioType();

		// -(BOOL)connectMyAudio:(BOOL)on;
		[Export ("connectMyAudio:")]
		bool ConnectMyAudio (bool on);

		// -(MobileRTCAudioOutput)myAudioOutputDescription;
		[Export("myAudioOutputDescription")]
		MobileRTCAudioOutput MyAudioOutputDescription();

		// -(BOOL)isMyAudioMuted;
		[Export("isMyAudioMuted")]
		bool IsMyAudioMuted();

		// -(BOOL)canUnmuteMyAudio;
		[Export("canUnmuteMyAudio")]
		bool CanUnmuteMyAudio();

		// -(BOOL)isMuteOnEntryOn;
		[Export("isMuteOnEntryOn")]
		bool IsMuteOnEntryOn();

		// -(BOOL)muteOnEntry:(BOOL)on;
		[Export ("muteOnEntry:")]
		bool MuteOnEntry (bool on);

		// -(BOOL)isUserAudioMuted:(NSUInteger)userID;
		[Export ("isUserAudioMuted:")]
		bool IsUserAudioMuted (nuint userID);

		// -(BOOL)muteUserAudio:(BOOL)mute withUID:(NSUInteger)userID;
		[Export ("muteUserAudio:withUID:")]
		bool MuteUserAudio (bool mute, nuint userID);

		// -(BOOL)muteAllUserAudio:(BOOL)allowSelfUnmute;
		[Export ("muteAllUserAudio:")]
		bool MuteAllUserAudio (bool allowSelfUnmute);

		// -(BOOL)askAllToUnmute;
		[Export("askAllToUnmute")]
		bool AskAllToUnmute();

		// -(BOOL)isSupportedVOIP;
		[Export("isSupportedVOIP")]
		bool IsSupportedVOIP();

		// -(BOOL)isPlayChimeOn;
		[Export("isPlayChimeOn")]
		bool IsPlayChimeOn();

		// -(BOOL)playChime:(BOOL)on;
		[Export ("playChime:")]
		bool PlayChime (bool on);

		// -(MobileRTCAudioError)muteMyAudio:(BOOL)mute;
		[Export ("muteMyAudio:")]
		MobileRTCAudioError MuteMyAudio (bool mute);

		// -(MobileRTCAudioError)switchMyAudioSource;
		[Export("switchMyAudioSource")]
		MobileRTCAudioError SwitchMyAudioSource();

		// -(void)resetMeetingAudioSession;
		[Export ("resetMeetingAudioSession")]
		void ResetMeetingAudioSession ();

		// -(void)resetMeetingAudioForCallKitHeld;
		[Export ("resetMeetingAudioForCallKitHeld")]
		void ResetMeetingAudioForCallKitHeld ();

		// -(BOOL)isIncomingAudioStopped;
		[Export("isIncomingAudioStopped")]
		bool IsIncomingAudioStopped();

		// -(MobileRTCSDKError)stopIncomingAudio:(BOOL)enabled;
		[Export ("stopIncomingAudio:")]
		MobileRTCSDKError StopIncomingAudio (bool enabled);

		// -(int)getSupportedMeetingAudioType;
		[Export("getSupportedMeetingAudioType")]
		int SupportedMeetingAudioType();
	}

	// @interface Video (MobileRTCMeetingService)
	[Category]
	[BaseType (typeof(MobileRTCMeetingService))]
	interface MobileRTCMeetingService_Video
	{
		// -(BOOL)isSendingMyVideo;
		[Export("isSendingMyVideo")]
		bool IsSendingMyVideo();

		// -(BOOL)canUnmuteMyVideo;
		[Export("canUnmuteMyVideo")]
		bool CanUnmuteMyVideo();

		// -(MobileRTCSDKError)muteMyVideo:(BOOL)mute;
		[Export ("muteMyVideo:")]
		MobileRTCSDKError MuteMyVideo (bool mute);

		// -(BOOL)rotateMyVideo:(UIDeviceOrientation)rotation;
		[Export ("rotateMyVideo:")]
		bool RotateMyVideo (UIDeviceOrientation rotation);

		// -(BOOL)isUserSpotlighted:(NSUInteger)userId;
		[Export ("isUserSpotlighted:")]
		bool IsUserSpotlighted (nuint userId);

		// -(BOOL)spotlightVideo:(BOOL)on withUser:(NSUInteger)userId;
		[Export ("spotlightVideo:withUser:")]
		bool SpotlightVideo (bool on, nuint userId);

		// -(BOOL)unSpotlightAllVideos;
		[Export("unSpotlightAllVideos")]
		bool UnSpotlightAllVideos();

		// -(NSArray<NSNumber *> * _Nullable)getSpotLightedVideoUserList;
		[Export("getSpotLightedVideoUserList")]
		NSNumber[] SpotLightedVideoUserList();

		// -(BOOL)isUserPinned:(NSUInteger)userId;
		[Export ("isUserPinned:")]
		bool IsUserPinned (nuint userId);

		// -(BOOL)pinVideo:(BOOL)on withUser:(NSUInteger)userId;
		[Export ("pinVideo:withUser:")]
		bool PinVideo (bool on, nuint userId);

		// -(BOOL)isUserVideoSending:(NSUInteger)userID;
		[Export ("isUserVideoSending:")]
		bool IsUserVideoSending (nuint userID);

		// -(BOOL)stopUserVideo:(NSUInteger)userID;
		[Export ("stopUserVideo:")]
		bool StopUserVideo (nuint userID);

		// -(BOOL)askUserStartVideo:(NSUInteger)userID;
		[Export ("askUserStartVideo:")]
		bool AskUserStartVideo (nuint userID);

		// -(CGSize)getUserVideoSize:(NSUInteger)userID;
		[Export ("getUserVideoSize:")]
		CGSize GetUserVideoSize (nuint userID);

		// -(BOOL)isBackCamera;
		[Export("isBackCamera")]
		bool IsBackCamera();

		// -(MobileRTCCameraError)switchMyCamera;
		[Export("switchMyCamera")]
		MobileRTCCameraError SwitchMyCamera();

		// -(BOOL)isSupportFollowHostVideoOrder;
		[Export("isSupportFollowHostVideoOrder")]
		bool IsSupportFollowHostVideoOrder();

		// -(BOOL)isFollowHostVideoOrderOn;
		[Export("isFollowHostVideoOrderOn")]
		bool IsFollowHostVideoOrderOn();

		// -(NSArray<NSNumber *> * _Nullable)getVideoOrderList;
		[Export("getVideoOrderList")]
		NSNumber[] VideoOrderList();

		// -(MobileRTCSDKError)stopIncomingVideo:(BOOL)enable;
		[Export ("stopIncomingVideo:")]
		MobileRTCSDKError StopIncomingVideo (bool enable);

		// -(BOOL)isIncomingVideoStoped;
		[Export("isIncomingVideoStoped")]
		bool IsIncomingVideoStoped();

		// -(BOOL)isStopIncomingVideoSupported;
		[Export("isStopIncomingVideoSupported")]
		bool IsStopIncomingVideoSupported();

		// -(MobileRTCSDKError)enableVideoAutoFraming:(MobileRTCAutoFramingParameter * _Nullable)setting forMode:(MobileRTCAutoFramingMode)mode;
		[Export ("enableVideoAutoFraming:forMode:")]
		MobileRTCSDKError EnableVideoAutoFraming ([NullAllowed] MobileRTCAutoFramingParameter setting, MobileRTCAutoFramingMode mode);

		// -(MobileRTCSDKError)disableVideoAutoFraming;
		[Export("disableVideoAutoFraming")]
		MobileRTCSDKError DisableVideoAutoFraming();

		// -(BOOL)isVideoAutoFramingEnabled;
		[Export("isVideoAutoFramingEnabled")]
		bool IsVideoAutoFramingEnabled();

		// -(MobileRTCAutoFramingMode)getVideoAutoFramingMode;
		[Export("getVideoAutoFramingMode")]
		MobileRTCAutoFramingMode VideoAutoFramingMode();

		// -(MobileRTCSDKError)setVideoAutoFramingMode:(MobileRTCAutoFramingMode)mode;
		[Export ("setVideoAutoFramingMode:")]
		MobileRTCSDKError SetVideoAutoFramingMode (MobileRTCAutoFramingMode mode);

		// -(MobileRTCSDKError)setVideoAutoFramingRatio:(CGFloat)ratio;
		[Export ("setVideoAutoFramingRatio:")]
		MobileRTCSDKError SetVideoAutoFramingRatio (nfloat ratio);

		// -(MobileRTCSDKError)setFaceRecognitionFailStrategy:(MobileRTCFaceRecognitionFailStrategy)strategy;
		[Export ("setFaceRecognitionFailStrategy:")]
		MobileRTCSDKError SetFaceRecognitionFailStrategy (MobileRTCFaceRecognitionFailStrategy strategy);

		// -(MobileRTCAutoFramingParameter * _Nullable)getVideoAutoFramingSetting:(MobileRTCAutoFramingMode)mode;
		[Export ("getVideoAutoFramingSetting:")]
		[return: NullAllowed]
		MobileRTCAutoFramingParameter GetVideoAutoFramingSetting (MobileRTCAutoFramingMode mode);
	}

	// @interface User (MobileRTCMeetingService)
	[Category]
	[BaseType (typeof(MobileRTCMeetingService))]
	interface MobileRTCMeetingService_User
	{
		// -(BOOL)changeName:(NSString * _Nonnull)inputName withUserID:(NSUInteger)userId;
		[Export ("changeName:withUserID:")]
		bool ChangeName (string inputName, nuint userId);

		// -(NSArray<NSNumber *> * _Nullable)getInMeetingUserList;
		[Export("getInMeetingUserList")]
		NSNumber[] InMeetingUserList();

		// -(NSArray<NSNumber *> * _Nullable)getWebinarAttendeeList;
		[Export("getWebinarAttendeeList")]
		NSNumber[] WebinarAttendeeList();

		// -(MobileRTCMeetingUserInfo * _Nullable)userInfoByID:(NSUInteger)userId;
		[Export ("userInfoByID:")]
		[return: NullAllowed]
		MobileRTCMeetingUserInfo UserInfoByID (nuint userId);

		// -(MobileRTCMeetingWebinarAttendeeInfo * _Nullable)attendeeInfoByID:(NSUInteger)userId;
		[Export ("attendeeInfoByID:")]
		[return: NullAllowed]
		MobileRTCMeetingWebinarAttendeeInfo AttendeeInfoByID (nuint userId);

		// -(BOOL)makeHost:(NSUInteger)userId;
		[Export ("makeHost:")]
		bool MakeHost (nuint userId);

		// -(BOOL)removeUser:(NSUInteger)userId;
		[Export ("removeUser:")]
		bool RemoveUser (nuint userId);

		// -(NSUInteger)myselfUserID;
		[Export("myselfUserID")]
		nuint MyselfUserID();

		// -(NSUInteger)activeUserID;
		[Export("activeUserID")]
		nuint ActiveUserID();

		// -(NSUInteger)activeShareUserID;
		[Export("activeShareUserID")]
		nuint ActiveShareUserID();

		// -(BOOL)isSameUser:(NSUInteger)user1 compareTo:(NSUInteger)user2;
		[Export ("isSameUser:compareTo:")]
		bool IsSameUser (nuint user1, nuint user2);

		// -(BOOL)isHostUser:(NSUInteger)userID;
		[Export ("isHostUser:")]
		bool IsHostUser (nuint userID);

		// -(BOOL)isMyself:(NSUInteger)userID;
		[Export ("isMyself:")]
		bool IsMyself (nuint userID);

		// -(BOOL)isH323User:(NSUInteger)userID;
		[Export ("isH323User:")]
		bool IsH323User (nuint userID);

		// -(BOOL)raiseMyHand;
		[Export("raiseMyHand")]
		bool RaiseMyHand();

		// -(BOOL)lowerHand:(NSUInteger)userId;
		[Export ("lowerHand:")]
		bool LowerHand (nuint userId);

		// -(BOOL)lowerAllHand:(BOOL)isWebinarAttendee;
		[Export ("lowerAllHand:")]
		bool LowerAllHand (bool isWebinarAttendee);

		// -(BOOL)canClaimhost;
		[Export("canClaimhost")]
		bool CanClaimhost();

		// -(BOOL)reclaimHost;
		[Export("reclaimHost")]
		bool ReclaimHost();

		// -(BOOL)claimHostWithHostKey:(NSString * _Nonnull)hostKey;
		[Export ("claimHostWithHostKey:")]
		bool ClaimHostWithHostKey (string hostKey);

		// -(BOOL)assignCohost:(NSUInteger)userID;
		[Export ("assignCohost:")]
		bool AssignCohost (nuint userID);

		// -(BOOL)revokeCoHost:(NSUInteger)userID;
		[Export ("revokeCoHost:")]
		bool RevokeCoHost (nuint userID);

		// -(BOOL)canBeCoHost:(NSUInteger)userID;
		[Export ("canBeCoHost:")]
		bool CanBeCoHost (nuint userID);

		// -(BOOL)isRawLiveStreaming:(NSUInteger)userID;
		[Export ("isRawLiveStreaming:")]
		bool IsRawLiveStreaming (nuint userID);

		// -(BOOL)hasRawLiveStreamPrivilege:(NSUInteger)userID;
		[Export ("hasRawLiveStreamPrivilege:")]
		bool HasRawLiveStreamPrivilege (nuint userID);
	}

	// @interface Chat (MobileRTCMeetingService)
	[Category]
	[BaseType (typeof(MobileRTCMeetingService))]
	interface MobileRTCMeetingService_Chat
	{
		// -(BOOL)isChatDisabled;
		[Export("isChatDisabled")]
		bool IsChatDisabled();

		// -(BOOL)isPrivateChatDisabled;
		[Export("isPrivateChatDisabled")]
		bool IsPrivateChatDisabled();

		// -(BOOL)changeAttendeeChatPriviledge:(MobileRTCMeetingChatPriviledgeType)privilege;
		[Export ("changeAttendeeChatPriviledge:")]
		bool ChangeAttendeeChatPriviledge (MobileRTCMeetingChatPriviledgeType privilege);

		// -(MobileRTCMeetingChatPriviledgeType)getAttendeeChatPriviledge;
		[Export("getAttendeeChatPriviledge")]
		MobileRTCMeetingChatPriviledgeType AttendeeChatPriviledge();

		// -(BOOL)isMeetingChatLegalNoticeAvailable;
		[Export("isMeetingChatLegalNoticeAvailable")]
		bool IsMeetingChatLegalNoticeAvailable();

		// -(NSString * _Nullable)getChatLegalNoticesPrompt;
		[Export("getChatLegalNoticesPrompt")]
		string ChatLegalNoticesPrompt();

		// -(NSString * _Nullable)getChatLegalNoticesExplained;
		[Export("getChatLegalNoticesExplained")]
		string ChatLegalNoticesExplained();

		// -(MobileRTCMeetingChat * _Nullable)meetingChatByID:(NSString * _Nonnull)messageID;
		[Export ("meetingChatByID:")]
		[return: NullAllowed]
		MobileRTCMeetingChat MeetingChatByID (string messageID);

		// -(MobileRTCSendChatError)sendChatToUser:(NSUInteger)userID WithContent:(NSString * _Nonnull)content;
		[Export ("sendChatToUser:WithContent:")]
		MobileRTCSendChatError SendChatToUser (nuint userID, string content);

		// -(MobileRTCSendChatError)sendChatToGroup:(MobileRTCChatGroup)group WithContent:(NSString * _Nonnull)content;
		[Export ("sendChatToGroup:WithContent:")]
		MobileRTCSendChatError SendChatToGroup (MobileRTCChatGroup group, string content);

		// -(BOOL)deleteChatMessage:(NSString * _Nonnull)msgId;
		[Export ("deleteChatMessage:")]
		bool DeleteChatMessage (string msgId);

		// -(NSArray<NSString *> * _Nullable)getAllChatMessageID;
		[Export("getAllChatMessageID")]
		string[] AllChatMessageID();

		// -(BOOL)isChatMessageCanBeDeleted:(NSString * _Nonnull)msgId;
		[Export ("isChatMessageCanBeDeleted:")]
		bool IsChatMessageCanBeDeleted (string msgId);

		// -(BOOL)isShareMeetingChatLegalNoticeAvailable;
		[Export("isShareMeetingChatLegalNoticeAvailable")]
		bool IsShareMeetingChatLegalNoticeAvailable();

		// -(NSString * _Nullable)getShareMeetingChatStartedLegalNoticeContent;
		[Export("getShareMeetingChatStartedLegalNoticeContent")]
		string ShareMeetingChatStartedLegalNoticeContent();

		// -(NSString * _Nullable)getShareMeetingChatStoppedLegalNoticeContent;
		[Export("getShareMeetingChatStoppedLegalNoticeContent")]
		string ShareMeetingChatStoppedLegalNoticeContent();
	}

	// @interface MobileRTC3DAvatarImageInfo : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTC3DAvatarImageInfo
	{
		// @property (assign, nonatomic) BOOL isSelected;
		[Export ("isSelected")]
		bool IsSelected { get; set; }

		// @property (copy, nonatomic) NSString * imagePath;
		[Export ("imagePath")]
		string ImagePath { get; set; }

		// @property (copy, nonatomic) NSString * imageName;
		[Export ("imageName")]
		string ImageName { get; set; }

		// @property (assign, nonatomic) NSInteger index;
		[Export ("index")]
		nint Index { get; set; }

		// @property (assign, nonatomic) BOOL isLastUsed;
		[Export ("isLastUsed")]
		bool IsLastUsed { get; set; }
	}

	// @interface Avatar (MobileRTCMeetingService)
	[Category]
	[BaseType (typeof(MobileRTCMeetingService))]
	interface MobileRTCMeetingService_Avatar
	{
		// -(BOOL)is3DAvatarSupportedByDevice;
		[Export("is3DAvatarSupportedByDevice")]
		bool Is3DAvatarSupportedByDevice();

		// -(BOOL)is3DAvatarEnabled;
		[Export("is3DAvatarEnabled")]
		bool Is3DAvatarEnabled();

		// -(NSArray<MobileRTC3DAvatarImageInfo *> *)get3DAvatarImageList;
		[Export("get3DAvatarImageList")]
		MobileRTC3DAvatarImageInfo[] Get3DAvatarImageList();

		// -(MobileRTCSDKError)set3DAvatarImage:(MobileRTC3DAvatarImageInfo *)imageInfo;
		[Export ("set3DAvatarImage:")]
		MobileRTCSDKError Set3DAvatarImage (MobileRTC3DAvatarImageInfo imageInfo);

		// -(MobileRTCSDKError)showAvatar:(BOOL)bShow;
		[Export ("showAvatar:")]
		MobileRTCSDKError ShowAvatar (bool bShow);

		// -(BOOL)isShowAvatar;
		[Export("isShowAvatar")]
		bool IsShowAvatar();

		// -(MobileRTCSDKError)enable3DAvatarEffectForAllMeeting:(BOOL)enable;
		[Export ("enable3DAvatarEffectForAllMeeting:")]
		MobileRTCSDKError Enable3DAvatarEffectForAllMeeting (bool enable);

		// -(BOOL)is3DAvatarEffectForAllMeetingEnabled;
		[Export("is3DAvatarEffectForAllMeetingEnabled")]
		bool Is3DAvatarEffectForAllMeetingEnabled();
	}

	// @interface MobileRTCQAAnswerItem : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCQAAnswerItem
	{
		// -(NSDate * _Nullable)getTime;
		[NullAllowed, Export ("getTime")]
		NSDate Time { get; }

		// -(NSString * _Nullable)getText;
		[NullAllowed, Export ("getText")]
		string Text { get; }

		// -(NSString * _Nullable)getSenderName;
		[NullAllowed, Export ("getSenderName")]
		string SenderName { get; }

		// -(NSString * _Nullable)getQuestionId;
		[NullAllowed, Export ("getQuestionId")]
		string QuestionId { get; }

		// -(NSString * _Nullable)getAnswerID;
		[NullAllowed, Export ("getAnswerID")]
		string AnswerID { get; }

		// -(BOOL)isPrivate;
		[Export ("isPrivate")]
		bool IsPrivate { get; }

		// -(BOOL)isLiveAnswer;
		[Export ("isLiveAnswer")]
		bool IsLiveAnswer { get; }

		// -(BOOL)isSenderMyself;
		[Export ("isSenderMyself")]
		bool IsSenderMyself { get; }
	}

	// @interface MobileRTCQAItem : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCQAItem
	{
		// -(NSString * _Nullable)getQuestionId;
		[NullAllowed, Export ("getQuestionId")]
		string QuestionId { get; }

		// -(NSDate * _Nullable)getTime;
		[NullAllowed, Export ("getTime")]
		NSDate Time { get; }

		// -(NSString * _Nullable)getText;
		[NullAllowed, Export ("getText")]
		string Text { get; }

		// -(NSString * _Nullable)getSenderName;
		[NullAllowed, Export ("getSenderName")]
		string SenderName { get; }

		// -(BOOL)isAnonymous;
		[Export ("isAnonymous")]
		bool IsAnonymous { get; }

		// -(BOOL)isMarkedAsAnswered;
		[Export ("isMarkedAsAnswered")]
		bool IsMarkedAsAnswered { get; }

		// -(BOOL)isMarkedAsDismissed;
		[Export ("isMarkedAsDismissed")]
		bool IsMarkedAsDismissed { get; }

		// -(NSUInteger)getUpvoteNumber;
		[Export ("getUpvoteNumber")]
		nuint UpvoteNumber { get; }

		// -(BOOL)getHasLiveAnswers;
		[Export ("getHasLiveAnswers")]
		bool HasLiveAnswers { get; }

		// -(BOOL)getHasTextAnswers;
		[Export ("getHasTextAnswers")]
		bool HasTextAnswers { get; }

		// -(BOOL)isMySelfUpvoted;
		[Export ("isMySelfUpvoted")]
		bool IsMySelfUpvoted { get; }

		// -(BOOL)amILiveAnswering;
		[Export ("amILiveAnswering")]
		bool AmILiveAnswering { get; }

		// -(BOOL)isLiveAnswering;
		[Export ("isLiveAnswering")]
		bool IsLiveAnswering { get; }

		// -(NSString * _Nullable)getLiveAnswerName;
		[NullAllowed, Export ("getLiveAnswerName")]
		string LiveAnswerName { get; }

		// -(BOOL)isSenderMyself;
		[Export ("isSenderMyself")]
		bool IsSenderMyself { get; }

		// -(NSArray<MobileRTCQAAnswerItem *> * _Nullable)getAnswerlist;
		[NullAllowed, Export ("getAnswerlist")]
		MobileRTCQAAnswerItem[] Answerlist { get; }
	}

	// @interface Webinar (MobileRTCMeetingService)
	[Category]
	[BaseType (typeof(MobileRTCMeetingService))]
	interface MobileRTCMeetingService_Webinar
	{
		// -(BOOL)hasPromptAndDePromptPrivilege;
		[Export("hasPromptAndDePromptPrivilege")]
		bool HasPromptAndDePromptPrivilege();

		// -(BOOL)promptAttendee2Panelist:(NSUInteger)userID;
		[Export ("promptAttendee2Panelist:")]
		bool PromptAttendee2Panelist (nuint userID);

		// -(BOOL)dePromptPanelist2Attendee:(NSUInteger)userID;
		[Export ("dePromptPanelist2Attendee:")]
		bool DePromptPanelist2Attendee (nuint userID);

		// -(BOOL)changePanelistChatPrivilege:(MobileRTCPanelistChatPrivilegeType)privilege;
		[Export ("changePanelistChatPrivilege:")]
		bool ChangePanelistChatPrivilege (MobileRTCPanelistChatPrivilegeType privilege);

		// -(MobileRTCPanelistChatPrivilegeType)getPanelistChatPrivilege;
		[Export("getPanelistChatPrivilege")]
		MobileRTCPanelistChatPrivilegeType PanelistChatPrivilege();

		// -(BOOL)allowAttendeeChat:(MobileRTCChatAllowAttendeeChat)privilegeType;
		[Export ("allowAttendeeChat:")]
		bool AllowAttendeeChat (MobileRTCChatAllowAttendeeChat privilegeType);

		// -(MobileRTCChatAllowAttendeeChat)getWebinarAttendeeChatPrivilege;
		[Export("getWebinarAttendeeChatPrivilege")]
		MobileRTCChatAllowAttendeeChat WebinarAttendeeChatPrivilege();

		// -(BOOL)isAllowAttendeeTalk:(NSUInteger)userID;
		[Export ("isAllowAttendeeTalk:")]
		bool IsAllowAttendeeTalk (nuint userID);

		// -(BOOL)allowAttenddeTalk:(NSUInteger)userID allow:(BOOL)enable;
		[Export ("allowAttenddeTalk:allow:")]
		bool AllowAttenddeTalk (nuint userID, bool enable);

		// -(BOOL)isAllowPanelistStartVideo;
		[Export("isAllowPanelistStartVideo")]
		bool IsAllowPanelistStartVideo();

		// -(BOOL)allowPanelistStartVideo:(BOOL)enable;
		[Export ("allowPanelistStartVideo:")]
		bool AllowPanelistStartVideo (bool enable);

		// -(BOOL)isAllowAskQuestionAnonymously;
		[Export("isAllowAskQuestionAnonymously")]
		bool IsAllowAskQuestionAnonymously();

		// -(BOOL)allowAskQuestionAnonymously:(BOOL)enable;
		[Export ("allowAskQuestionAnonymously:")]
		bool AllowAskQuestionAnonymously (bool enable);

		// -(BOOL)isAllowAttendeeViewAllQuestion;
		[Export("isAllowAttendeeViewAllQuestion")]
		bool IsAllowAttendeeViewAllQuestion();

		// -(BOOL)allowAttendeeViewAllQuestion:(BOOL)enable;
		[Export ("allowAttendeeViewAllQuestion:")]
		bool AllowAttendeeViewAllQuestion (bool enable);

		// -(BOOL)isAllowAttendeeUpVoteQuestion;
		[Export("isAllowAttendeeUpVoteQuestion")]
		bool IsAllowAttendeeUpVoteQuestion();

		// -(BOOL)allowAttendeeUpVoteQuestion:(BOOL)enable;
		[Export ("allowAttendeeUpVoteQuestion:")]
		bool AllowAttendeeUpVoteQuestion (bool enable);

		// -(BOOL)isAllowCommentQuestion;
		[Export("isAllowCommentQuestion")]
		bool IsAllowCommentQuestion();

		// -(BOOL)allowCommentQuestion:(BOOL)enable;
		[Export ("allowCommentQuestion:")]
		bool AllowCommentQuestion (bool enable);

		// -(NSArray<MobileRTCQAItem *> * _Nullable)getAllQuestionList;
		[Export("getAllQuestionList")]
		MobileRTCQAItem[] AllQuestionList();

		// -(NSArray<MobileRTCQAItem *> * _Nullable)getMyQuestionList;
		[Export("getMyQuestionList")]
		MobileRTCQAItem[] MyQuestionList();

		// -(NSArray<MobileRTCQAItem *> * _Nullable)getOpenQuestionList;
		[Export("getOpenQuestionList")]
		MobileRTCQAItem[] OpenQuestionList();

		// -(NSArray<MobileRTCQAItem *> * _Nullable)getDismissedQuestionList;
		[Export("getDismissedQuestionList")]
		MobileRTCQAItem[] DismissedQuestionList();

		// -(NSArray<MobileRTCQAItem *> * _Nullable)getAnsweredQuestionList;
		[Export("getAnsweredQuestionList")]
		MobileRTCQAItem[] AnsweredQuestionList();

		// -(int)getALLQuestionCount;
		[Export("getALLQuestionCount")]
		int ALLQuestionCount();

		// -(int)getMyQuestionCount;
		[Export("getMyQuestionCount")]
		int MyQuestionCount();

		// -(int)getOpenQuestionCount;
		[Export("getOpenQuestionCount")]
		int OpenQuestionCount();

		// -(int)getDismissedQuestionCount;
		[Export("getDismissedQuestionCount")]
		int DismissedQuestionCount();

		// -(int)getAnsweredQuestionCount;
		[Export("getAnsweredQuestionCount")]
		int AnsweredQuestionCount();

		// -(MobileRTCQAItem * _Nullable)getQuestion:(NSString * _Nonnull)questionID;
		[Export ("getQuestion:")]
		[return: NullAllowed]
		MobileRTCQAItem GetQuestion (string questionID);

		// -(MobileRTCQAAnswerItem * _Nullable)getAnswer:(NSString * _Nonnull)answerID;
		[Export ("getAnswer:")]
		[return: NullAllowed]
		MobileRTCQAAnswerItem GetAnswer (string answerID);

		// -(BOOL)addQuestion:(NSString * _Nonnull)content anonymous:(BOOL)anonymous;
		[Export ("addQuestion:anonymous:")]
		bool AddQuestion (string content, bool anonymous);

		// -(BOOL)answerQuestionPrivate:(NSString * _Nonnull)questionID answerContent:(NSString * _Nonnull)answerContent;
		[Export ("answerQuestionPrivate:answerContent:")]
		bool AnswerQuestionPrivate (string questionID, string answerContent);

		// -(BOOL)answerQuestionPublic:(NSString * _Nonnull)questionID answerContent:(NSString * _Nonnull)answerContent;
		[Export ("answerQuestionPublic:answerContent:")]
		bool AnswerQuestionPublic (string questionID, string answerContent);

		// -(BOOL)commentQuestion:(NSString * _Nonnull)questionID commentContent:(NSString * _Nonnull)commentContent;
		[Export ("commentQuestion:commentContent:")]
		bool CommentQuestion (string questionID, string commentContent);

		// -(BOOL)dismissQuestion:(NSString * _Nonnull)questionID;
		[Export ("dismissQuestion:")]
		bool DismissQuestion (string questionID);

		// -(BOOL)reopenQuestion:(NSString * _Nonnull)questionID;
		[Export ("reopenQuestion:")]
		bool ReopenQuestion (string questionID);

		// -(BOOL)voteupQuestion:(NSString * _Nonnull)questionID voteup:(BOOL)voteup;
		[Export ("voteupQuestion:voteup:")]
		bool VoteupQuestion (string questionID, bool voteup);

		// -(BOOL)startLiving:(NSString * _Nonnull)questionID;
		[Export ("startLiving:")]
		bool StartLiving (string questionID);

		// -(BOOL)endLiving:(NSString * _Nonnull)questionID;
		[Export ("endLiving:")]
		bool EndLiving (string questionID);

		// -(BOOL)deleteQuestion:(NSString * _Nonnull)questionID;
		[Export ("deleteQuestion:")]
		bool DeleteQuestion (string questionID);

		// -(BOOL)deleteAnswer:(NSString * _Nonnull)answerID;
		[Export ("deleteAnswer:")]
		bool DeleteAnswer (string answerID);

		// -(BOOL)isQALegalNoticeAvailable;
		[Export("isQALegalNoticeAvailable")]
		bool IsQALegalNoticeAvailable();

		// -(BOOL)isWebinarEmojiReactionAllowed;
		[Export("isWebinarEmojiReactionAllowed")]
		bool IsWebinarEmojiReactionAllowed();

		// -(MobileRTCSDKError)allowWebinarEmojiReaction;
		[Export("allowWebinarEmojiReaction")]
		MobileRTCSDKError AllowWebinarEmojiReaction();

		// -(MobileRTCSDKError)disallowWebinarEmojiReaction;
		[Export("disallowWebinarEmojiReaction")]
		MobileRTCSDKError DisallowWebinarEmojiReaction();

		// -(BOOL)isAttendeeRaiseHandAllowed;
		[Export("isAttendeeRaiseHandAllowed")]
		bool IsAttendeeRaiseHandAllowed();

		// -(MobileRTCSDKError)allowAttendeeRaiseHand;
		[Export("allowAttendeeRaiseHand")]
		MobileRTCSDKError AllowAttendeeRaiseHand();

		// -(MobileRTCSDKError)disallowAttendeeRaiseHand;
		[Export("disallowAttendeeRaiseHand")]
		MobileRTCSDKError DisallowAttendeeRaiseHand();

		// -(BOOL)isAttendeeViewTheParticipantCountAllowed;
		[Export("isAttendeeViewTheParticipantCountAllowed")]
		bool IsAttendeeViewTheParticipantCountAllowed();

		// -(MobileRTCSDKError)allowAttendeeViewTheParticipantCount;
		[Export("allowAttendeeViewTheParticipantCount")]
		MobileRTCSDKError AllowAttendeeViewTheParticipantCount();

		// -(MobileRTCSDKError)disallowAttendeeViewTheParticipantCount;
		[Export("disallowAttendeeViewTheParticipantCount")]
		MobileRTCSDKError DisallowAttendeeViewTheParticipantCount();

		// -(NSUInteger)getParticipantCount;
		[Export("getParticipantCount")]
		nuint ParticipantCount();

		// -(MobileRTCSDKError)setAttendeeViewMode:(MobileRTCAttendeeViewMode)mode;
		[Export ("setAttendeeViewMode:")]
		MobileRTCSDKError SetAttendeeViewMode (MobileRTCAttendeeViewMode mode);

		// -(MobileRTCAttendeeViewMode)getAttendeeViewMode;
		[Export("getAttendeeViewMode")]
		MobileRTCAttendeeViewMode AttendeeViewMode();

		// -(NSString * _Nullable)getQALegalNoticesPrompt;
		[Export("getQALegalNoticesPrompt")]
		string QALegalNoticesPrompt();

		// -(NSString * _Nullable)getQALegalNoticesExplained;
		[Export("getQALegalNoticesExplained")]
		string QALegalNoticesExplained();

		// -(NSString * _Nullable)getPollLegalNoticesPrompt;
		[Export("getPollLegalNoticesPrompt")]
		string PollLegalNoticesPrompt();

		// -(BOOL)isPollingLegalNoticeAvailable;
		[Export("isPollingLegalNoticeAvailable")]
		bool IsPollingLegalNoticeAvailable();

		// -(NSString * _Nullable)getPollLegalNoticesExplained;
		[Export("getPollLegalNoticesExplained")]
		string PollLegalNoticesExplained();

		// -(NSString * _Nullable)getPollAnonymousLegalNoticesExplained;
		[Export("getPollAnonymousLegalNoticesExplained")]
		string PollAnonymousLegalNoticesExplained();

		// -(NSString * _Nullable)getWebinarRegistrationLegalNoticesPrompt;
		[Export("getWebinarRegistrationLegalNoticesPrompt")]
		string WebinarRegistrationLegalNoticesPrompt();

		// -(MobileRTCWebinarRegistLegalNoticeContent * _Nullable)getWebinarRegistrationLegalNoticesExplained;
		[Export("getWebinarRegistrationLegalNoticesExplained")]
		MobileRTCWebinarRegistLegalNoticeContent WebinarRegistrationLegalNoticesExplained();
	}

	// @interface MobileRTCVirtualBGImageInfo : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCVirtualBGImageInfo
	{
		// @property (assign, nonatomic) MobileRTCVBType vbType;
		[Export ("vbType", ArgumentSemantic.Assign)]
		MobileRTCVBType VbType { get; set; }

		// @property (assign, nonatomic) BOOL isSelect;
		[Export ("isSelect")]
		bool IsSelect { get; set; }

		// @property (retain, nonatomic) NSString * _Nullable imagePath;
		[NullAllowed, Export ("imagePath", ArgumentSemantic.Retain)]
		string ImagePath { get; set; }
	}

	// @interface VirtualBackground (MobileRTCMeetingService)
	[Category]
	[BaseType (typeof(MobileRTCMeetingService))]
	interface MobileRTCMeetingService_VirtualBackground
	{
		// @property (retain, nonatomic) UIView * _Nullable previewView;
		[Export("previewView", ArgumentSemantic.Retain)]
		UIView PreviewView();

		// -(BOOL)startPreviewWithFrame:(CGRect)frame;
		[Export ("startPreviewWithFrame:")]
		bool StartPreviewWithFrame (CGRect frame);

		// -(BOOL)isSupportVirtualBG;
		[Export("isSupportVirtualBG")]
		bool IsSupportVirtualBG();

		// -(BOOL)isSupportSmartVirtualBG;
		[Export("isSupportSmartVirtualBG")]
		bool IsSupportSmartVirtualBG();

		// -(NSArray<MobileRTCVirtualBGImageInfo *> * _Nonnull)getBGImageList;
		[Export("getBGImageList")]
		MobileRTCVirtualBGImageInfo[] BGImageList();

		// -(MobileRTCMeetError)addBGImage:(UIImage * _Nonnull)image;
		[Export ("addBGImage:")]
		MobileRTCMeetError AddBGImage (UIImage image);

		// -(MobileRTCMeetError)removeBGImage:(MobileRTCVirtualBGImageInfo * _Nonnull)bgImageInfo;
		[Export ("removeBGImage:")]
		MobileRTCMeetError RemoveBGImage (MobileRTCVirtualBGImageInfo bgImageInfo);

		// -(MobileRTCMeetError)useBGImage:(MobileRTCVirtualBGImageInfo * _Nonnull)bgImage;
		[Export ("useBGImage:")]
		MobileRTCMeetError UseBGImage (MobileRTCVirtualBGImageInfo bgImage);

		// -(MobileRTCMeetError)useNoneImage;
		[Export("useNoneImage")]
		MobileRTCMeetError UseNoneImage();

		// -(BOOL)isUsingGreenVB;
		[Export("isUsingGreenVB")]
		bool IsUsingGreenVB();

		// -(MobileRTCMeetError)enableGreenVB:(BOOL)enable;
		[Export ("enableGreenVB:")]
		MobileRTCMeetError EnableGreenVB (bool enable);

		// -(MobileRTCMeetError)selectGreenVBPoint:(CGPoint)point;
		[Export ("selectGreenVBPoint:")]
		MobileRTCMeetError SelectGreenVBPoint (CGPoint point);
	}

	// @interface MobileRTCInterpretationLanguage : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCInterpretationLanguage
	{
		// -(NSInteger)getLanguageID;
		[Export ("getLanguageID")]
		nint LanguageID { get; }

		// -(NSString * _Nullable)getLanguageAbbreviations;
		[NullAllowed, Export ("getLanguageAbbreviations")]
		string LanguageAbbreviations { get; }

		// -(NSString * _Nullable)getLanguageName;
		[NullAllowed, Export ("getLanguageName")]
		string LanguageName { get; }
	}

	// @interface MobileRTCMeetingInterpreter : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCMeetingInterpreter
	{
		// -(NSInteger)getUserID;
		[Export ("getUserID")]
		nint UserID { get; }

		// -(NSInteger)getLanguageID1;
		[Export ("getLanguageID1")]
		nint LanguageID1 { get; }

		// -(NSInteger)getLanguageID2;
		[Export ("getLanguageID2")]
		nint LanguageID2 { get; }

		// -(BOOL)isAvailable;
		[Export ("isAvailable")]
		bool IsAvailable { get; }
	}

	// @interface Interpretation (MobileRTCMeetingService)
	[Category]
	[BaseType (typeof(MobileRTCMeetingService))]
	interface MobileRTCMeetingService_Interpretation
	{
		// -(BOOL)isInterpretationEnabled;
		[Export("isInterpretationEnabled")]
		bool IsInterpretationEnabled();

		// -(BOOL)isInterpretationStarted;
		[Export("isInterpretationStarted")]
		bool IsInterpretationStarted();

		// -(BOOL)isInterpreter;
		[Export("isInterpreter")]
		bool IsInterpreter();

		// -(MobileRTCInterpretationLanguage * _Nullable)getInterpretationLanguageByID:(NSInteger)lanID;
		[Export ("getInterpretationLanguageByID:")]
		[return: NullAllowed]
		MobileRTCInterpretationLanguage GetInterpretationLanguageByID (nint lanID);

		// -(NSArray<MobileRTCInterpretationLanguage *> * _Nullable)getAllLanguageList;
		[Export("getAllLanguageList")]
		MobileRTCInterpretationLanguage[] AllLanguageList();

		// -(NSArray<MobileRTCMeetingInterpreter *> * _Nullable)getInterpreterList;
		[Export("getInterpreterList")]
		MobileRTCMeetingInterpreter[] InterpreterList();

		// -(BOOL)addInterpreter:(NSUInteger)userID lan1:(NSInteger)lanID1 andLan2:(NSInteger)lanID2;
		[Export ("addInterpreter:lan1:andLan2:")]
		bool AddInterpreter (nuint userID, nint lanID1, nint lanID2);

		// -(BOOL)removeInterpreter:(NSUInteger)userID;
		[Export ("removeInterpreter:")]
		bool RemoveInterpreter (nuint userID);

		// -(BOOL)modifyInterpreter:(NSUInteger)userID lan1:(NSInteger)lanID1 andLan2:(NSInteger)lanID2;
		[Export ("modifyInterpreter:lan1:andLan2:")]
		bool ModifyInterpreter (nuint userID, nint lanID1, nint lanID2);

		// -(BOOL)startInterpretation;
		[Export("startInterpretation")]
		bool StartInterpretation();

		// -(BOOL)stopInterpretation;
		[Export("stopInterpretation")]
		bool StopInterpretation();

		// -(NSArray<MobileRTCInterpretationLanguage *> * _Nullable)getAvailableLanguageList;
		[Export("getAvailableLanguageList")]
		MobileRTCInterpretationLanguage[] AvailableLanguageList();

		// -(BOOL)joinLanguageChannel:(NSInteger)lanID;
		[Export ("joinLanguageChannel:")]
		bool JoinLanguageChannel (nint lanID);

		// -(NSInteger)getJoinedLanguageID;
		[Export("getJoinedLanguageID")]
		nint JoinedLanguageID();

		// -(BOOL)turnOffMajorAudio;
		[Export("turnOffMajorAudio")]
		bool TurnOffMajorAudio();

		// -(BOOL)turnOnMajorAudio;
		[Export("turnOnMajorAudio")]
		bool TurnOnMajorAudio();

		// -(BOOL)isMajorAudioTurnOff;
		[Export("isMajorAudioTurnOff")]
		bool IsMajorAudioTurnOff();

		// -(NSArray<MobileRTCInterpretationLanguage *> * _Nullable)getInterpreterLans;
		[Export("getInterpreterLans")]
		MobileRTCInterpretationLanguage[] InterpreterLans();

		// -(BOOL)setInterpreterActiveLan:(NSInteger)activeLanID;
		[Export ("setInterpreterActiveLan:")]
		bool SetInterpreterActiveLan (nint activeLanID);

		// -(NSInteger)getInterpreterActiveLan;
		[Export("getInterpreterActiveLan")]
		nint InterpreterActiveLan();

		// -(NSArray<MobileRTCInterpretationLanguage *> * _Nullable)getInterpreterAvailableLanguages;
		[Export("getInterpreterAvailableLanguages")]
		MobileRTCInterpretationLanguage[] InterpreterAvailableLanguages();

		// -(BOOL)setInterpreterListenLan:(NSInteger)lanID;
		[Export ("setInterpreterListenLan:")]
		bool SetInterpreterListenLan (nint lanID);

		// -(NSInteger)getInterpreterListenLan;
		[Export("getInterpreterListenLan")]
		nint InterpreterListenLan();
	}

	// @interface MobileRTCSignInterpreterLanguage : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCSignInterpreterLanguage
	{
		// @property (copy, nonatomic) NSString * _Nullable languageName;
		[NullAllowed, Export ("languageName")]
		string LanguageName { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable languageID;
		[NullAllowed, Export ("languageID")]
		string LanguageID { get; set; }
	}

	// @interface MobileRTCSignInterpreter : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCSignInterpreter
	{
		// @property (assign, nonatomic) NSUInteger userID;
		[Export ("userID")]
		nuint UserID { get; set; }

		// @property (assign, nonatomic) BOOL available;
		[Export ("available")]
		bool Available { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable userName;
		[NullAllowed, Export ("userName")]
		string UserName { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable email;
		[NullAllowed, Export ("email")]
		string Email { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable languageName;
		[NullAllowed, Export ("languageName")]
		string LanguageName { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable languageID;
		[NullAllowed, Export ("languageID")]
		string LanguageID { get; set; }
	}

	// @interface SignInterpreter (MobileRTCMeetingService)
	[Category]
	[BaseType (typeof(MobileRTCMeetingService))]
	interface MobileRTCMeetingService_SignInterpreter
	{
		// -(BOOL)isSignInterpretationEnabled;
		[Export("isSignInterpretationEnabled")]
		bool IsSignInterpretationEnabled();

		// -(MobileRTCSignInterpretationStatus)getSignInterpretationStatus;
		[Export("getSignInterpretationStatus")]
		MobileRTCSignInterpretationStatus SignInterpretationStatus();

		// -(BOOL)isSignInterpreter;
		[Export("isSignInterpreter")]
		bool IsSignInterpreter();

		// -(MobileRTCSignInterpreterLanguage * _Nullable)getSignInterpretationLanguageInfoByID:(NSString * _Nullable)signLanguageID;
		[Export ("getSignInterpretationLanguageInfoByID:")]
		[return: NullAllowed]
		MobileRTCSignInterpreterLanguage GetSignInterpretationLanguageInfoByID ([NullAllowed] string signLanguageID);

		// -(NSArray<MobileRTCSignInterpreterLanguage *> * _Nullable)getAvailableSignLanguageInfoList;
		[Export("getAvailableSignLanguageInfoList")]
		MobileRTCSignInterpreterLanguage[] AvailableSignLanguageInfoList();

		// -(NSArray<MobileRTCSignInterpreterLanguage *> * _Nullable)getAllSupportedSignLanguageInfoList;
		[Export("getAllSupportedSignLanguageInfoList")]
		MobileRTCSignInterpreterLanguage[] AllSupportedSignLanguageInfoList();

		// -(NSArray<MobileRTCSignInterpreter *> * _Nullable)getSignInterpreterList;
		[Export("getSignInterpreterList")]
		MobileRTCSignInterpreter[] SignInterpreterList();

		// -(MobileRTCSDKError)addSignInterpreter:(NSUInteger)userID signLanId:(NSString * _Nullable)signLanID;
		[Export ("addSignInterpreter:signLanId:")]
		MobileRTCSDKError AddSignInterpreter (nuint userID, [NullAllowed] string signLanID);

		// -(MobileRTCSDKError)removeSignInterpreter:(NSUInteger)userID;
		[Export ("removeSignInterpreter:")]
		MobileRTCSDKError RemoveSignInterpreter (nuint userID);

		// -(MobileRTCSDKError)modifySignInterpreter:(NSUInteger)userID signLanId:(NSString * _Nullable)signLanID;
		[Export ("modifySignInterpreter:signLanId:")]
		MobileRTCSDKError ModifySignInterpreter (nuint userID, [NullAllowed] string signLanID);

		// -(BOOL)canStartSignInterpretation;
		[Export("canStartSignInterpretation")]
		bool CanStartSignInterpretation();

		// -(MobileRTCSDKError)startSignInterpretation;
		[Export("startSignInterpretation")]
		MobileRTCSDKError StartSignInterpretation();

		// -(MobileRTCSDKError)stopSignInterpretation;
		[Export("stopSignInterpretation")]
		MobileRTCSDKError StopSignInterpretation();

		// -(MobileRTCSDKError)requestSignLanuageInterpreterToTalk:(NSUInteger)userID allowToTalk:(BOOL)allowToTalk;
		[Export ("requestSignLanuageInterpreterToTalk:allowToTalk:")]
		MobileRTCSDKError RequestSignLanuageInterpreterToTalk (nuint userID, bool allowToTalk);

		// -(BOOL)isAllowSignLanuageInterpreterToTalk:(NSUInteger)userID;
		[Export ("isAllowSignLanuageInterpreterToTalk:")]
		bool IsAllowSignLanuageInterpreterToTalk (nuint userID);

		// -(NSString * _Nullable)getSignInterpreterAssignedLanID;
		[Export("getSignInterpreterAssignedLanID")]
		string SignInterpreterAssignedLanID();

		// -(MobileRTCSDKError)joinSignLanguageChannel:(NSString * _Nullable)signLanID;
		[Export ("joinSignLanguageChannel:")]
		MobileRTCSDKError JoinSignLanguageChannel ([NullAllowed] string signLanID);

		// -(MobileRTCSDKError)leaveSignLanguageChannel;
		[Export("leaveSignLanguageChannel")]
		MobileRTCSDKError LeaveSignLanguageChannel();
	}

	// @interface BO (MobileRTCMeetingService)
	[Category]
	[BaseType (typeof(MobileRTCMeetingService))]
	interface MobileRTCMeetingService_BO
	{
		// -(MobileRTCBOCreator * _Nullable)getCreatorHelper;
		[Export("getCreatorHelper")]
		MobileRTCBOCreator CreatorHelper();

		// -(MobileRTCBOAdmin * _Nullable)getAdminHelper;
		[Export("getAdminHelper")]
		MobileRTCBOAdmin AdminHelper();

		// -(MobileRTCBOAssistant * _Nullable)getAssistantHelper;
		[Export("getAssistantHelper")]
		MobileRTCBOAssistant AssistantHelper();

		// -(MobileRTCBOAttendee * _Nullable)getAttedeeHelper;
		[Export("getAttedeeHelper")]
		MobileRTCBOAttendee AttedeeHelper();

		// -(MobileRTCBOData * _Nullable)getDataHelper;
		[Export("getDataHelper")]
		MobileRTCBOData DataHelper();

		// -(BOOL)isBOMeetingStarted;
		[Export("isBOMeetingStarted")]
		bool IsBOMeetingStarted();

		// -(BOOL)isBOMeetingEnabled;
		[Export("isBOMeetingEnabled")]
		bool IsBOMeetingEnabled();

		// -(BOOL)isInBOMeeting;
		[Export("isInBOMeeting")]
		bool IsInBOMeeting();

		// -(BOOL)isBroadcastingVoiceToBO;
		[Export("isBroadcastingVoiceToBO")]
		bool IsBroadcastingVoiceToBO();

		// -(MobileRTCBOStatus)getBOStatus;
		[Export("getBOStatus")]
		MobileRTCBOStatus BOStatus();
	}

	// @interface Reaction (MobileRTCMeetingService)
	[Category]
	[BaseType (typeof(MobileRTCMeetingService))]
	interface MobileRTCMeetingService_Reaction
	{
		// -(BOOL)isEmojiReactionEnabled;
		[Export("isEmojiReactionEnabled")]
		bool IsEmojiReactionEnabled();

		// -(MobileRTCSDKError)sendEmojiReaction:(MobileRTCEmojiReactionType)type;
		[Export ("sendEmojiReaction:")]
		MobileRTCSDKError SendEmojiReaction (MobileRTCEmojiReactionType type);

		// -(MobileRTCSDKError)sendEmojiFeedback:(MobileRTCEmojiFeedbackType)type;
		[Export ("sendEmojiFeedback:")]
		MobileRTCSDKError SendEmojiFeedback (MobileRTCEmojiFeedbackType type);

		// -(MobileRTCSDKError)cancelEmojiFeedback;
		[Export("cancelEmojiFeedback")]
		MobileRTCSDKError CancelEmojiFeedback();
	}

	// @interface MobileRTCLiveTranscriptionMessageInfo : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCLiveTranscriptionMessageInfo
	{
		// @property (copy, nonatomic) NSString * messageID;
		[Export ("messageID")]
		string MessageID { get; set; }

		// @property (assign, nonatomic) NSInteger speakerID;
		[Export ("speakerID")]
		nint SpeakerID { get; set; }

		// @property (copy, nonatomic) NSString * speakerName;
		[Export ("speakerName")]
		string SpeakerName { get; set; }

		// @property (copy, nonatomic) NSString * messageContent;
		[Export ("messageContent")]
		string MessageContent { get; set; }

		// @property (assign, nonatomic) NSInteger timeStamp;
		[Export ("timeStamp")]
		nint TimeStamp { get; set; }

		// @property (assign, nonatomic) MobileRTCLiveTranscriptionOperationType messageType;
		[Export ("messageType", ArgumentSemantic.Assign)]
		MobileRTCLiveTranscriptionOperationType MessageType { get; set; }
	}

	// @interface LiveTranscription (MobileRTCMeetingService)
	[Category]
	[BaseType (typeof(MobileRTCMeetingService))]
	interface MobileRTCMeetingService_LiveTranscription
	{
		// -(BOOL)isMeetingSupportCC;
		[Export("isMeetingSupportCC")]
		bool IsMeetingSupportCC();

		// -(BOOL)canDisableCaptions;
		[Export("canDisableCaptions")]
		bool CanDisableCaptions();

		// -(MobileRTCSDKError)enableCaptions:(BOOL)bEnable;
		[Export ("enableCaptions:")]
		MobileRTCSDKError EnableCaptions (bool bEnable);

		// -(BOOL)isCaptionsEnabled;
		[Export("isCaptionsEnabled")]
		bool IsCaptionsEnabled();

		// -(BOOL)canBeAssignedToSendCC:(NSUInteger)userId;
		[Export ("canBeAssignedToSendCC:")]
		bool CanBeAssignedToSendCC (nuint userId);

		// -(BOOL)assignCCPrivilege:(NSUInteger)userId;
		[Export ("assignCCPrivilege:")]
		bool AssignCCPrivilege (nuint userId);

		// -(BOOL)withdrawCCPrivilege:(NSUInteger)userId;
		[Export ("withdrawCCPrivilege:")]
		bool WithdrawCCPrivilege (nuint userId);

		// -(BOOL)canAssignOthersToSendCC;
		[Export("canAssignOthersToSendCC")]
		bool CanAssignOthersToSendCC();

		// -(BOOL)enableMeetingManualCaption:(BOOL)bEnable;
		[Export ("enableMeetingManualCaption:")]
		bool EnableMeetingManualCaption (bool bEnable);

		// -(BOOL)isMeetingManualCaptionEnabled;
		[Export("isMeetingManualCaptionEnabled")]
		bool IsMeetingManualCaptionEnabled();

		// -(BOOL)isLiveTranscriptionFeatureEnabled;
		[Export("isLiveTranscriptionFeatureEnabled")]
		bool IsLiveTranscriptionFeatureEnabled();

		// -(MobileRTCLiveTranscriptionStatus)getLiveTranscriptionStatus;
		[Export("getLiveTranscriptionStatus")]
		MobileRTCLiveTranscriptionStatus LiveTranscriptionStatus();

		// -(BOOL)canStartLiveTranscription;
		[Export("canStartLiveTranscription")]
		bool CanStartLiveTranscription();

		// -(BOOL)startLiveTranscription;
		[Export("startLiveTranscription")]
		bool StartLiveTranscription();

		// -(BOOL)stopLiveTranscription;
		[Export("stopLiveTranscription")]
		bool StopLiveTranscription();

		// -(BOOL)enableRequestLiveTranscription:(BOOL)enable;
		[Export ("enableRequestLiveTranscription:")]
		bool EnableRequestLiveTranscription (bool enable);

		// -(BOOL)isRequestToStartLiveTranscriptionEnabled;
		[Export("isRequestToStartLiveTranscriptionEnabled")]
		bool IsRequestToStartLiveTranscriptionEnabled();

		// -(BOOL)requestToStartLiveTranscription:(BOOL)requestAnonymous;
		[Export ("requestToStartLiveTranscription:")]
		bool RequestToStartLiveTranscription (bool requestAnonymous);

		// -(BOOL)isMultiLanguageTranscriptionEnabled;
		[Export("isMultiLanguageTranscriptionEnabled")]
		bool IsMultiLanguageTranscriptionEnabled();

		// -(BOOL)isTextLiveTranslationEnabled;
		[Export("isTextLiveTranslationEnabled")]
		bool IsTextLiveTranslationEnabled();

		// -(MobileRTCSDKError)enableReceiveSpokenlLanguageContent:(BOOL)enabled;
		[Export ("enableReceiveSpokenlLanguageContent:")]
		MobileRTCSDKError EnableReceiveSpokenlLanguageContent (bool enabled);

		// -(BOOL)isReceiveSpokenLanguageContentEnabled;
		[Export("isReceiveSpokenLanguageContentEnabled")]
		bool IsReceiveSpokenLanguageContentEnabled();

		// -(NSArray<MobileRTCLiveTranscriptionLanguage *> *)getAvailableMeetingSpokenLanguages;
		[Export("getAvailableMeetingSpokenLanguages")]
		MobileRTCLiveTranscriptionLanguage[] AvailableMeetingSpokenLanguages();

		// -(BOOL)setMeetingSpokenLanguage:(NSInteger)languageID;
		[Export ("setMeetingSpokenLanguage:")]
		bool SetMeetingSpokenLanguage (nint languageID);

		// -(MobileRTCLiveTranscriptionLanguage *)getMeetingSpokenLanguage;
		[Export("getMeetingSpokenLanguage")]
		MobileRTCLiveTranscriptionLanguage MeetingSpokenLanguage();

		// -(NSArray<MobileRTCLiveTranscriptionLanguage *> *)getAvailableTranslationLanguages;
		[Export("getAvailableTranslationLanguages")]
		MobileRTCLiveTranscriptionLanguage[] AvailableTranslationLanguages();

		// -(BOOL)setTranslationLanguage:(NSInteger)languageID;
		[Export ("setTranslationLanguage:")]
		bool SetTranslationLanguage (nint languageID);

		// -(MobileRTCLiveTranscriptionLanguage *)getTranslationLanguage;
		[Export("getTranslationLanguage")]
		MobileRTCLiveTranscriptionLanguage TranslationLanguage();
	}

	// @interface RawArchiving (MobileRTCMeetingService)
	[Category]
	[BaseType (typeof(MobileRTCMeetingService))]
	interface MobileRTCMeetingService_RawArchiving
	{
		// -(BOOL)startRawArchiving;
		[Export("startRawArchiving")]
		bool StartRawArchiving();

		// -(BOOL)stopRawArchiving;
		[Export("stopRawArchiving")]
		bool StopRawArchiving();
	}

	// @interface Phone (MobileRTCMeetingService)
	[Category]
	[BaseType (typeof(MobileRTCMeetingService))]
	interface MobileRTCMeetingService_Phone
	{
		// -(BOOL)isDialOutSupported;
		[Export("isDialOutSupported")]
		bool IsDialOutSupported();

		// -(NSArray<MobileRTCCallCountryCode *> * _Nonnull)getSupportCountryInfo;
		[Export("getSupportCountryInfo")]
		MobileRTCCallCountryCode[] SupportCountryInfo();

		// -(BOOL)isDialOutInProgress;
		[Export("isDialOutInProgress")]
		bool IsDialOutInProgress();

		// -(BOOL)dialOut:(NSString * _Nonnull)phone isCallMe:(BOOL)me withName:(NSString * _Nullable)username;
		[Export ("dialOut:isCallMe:withName:")]
		bool DialOut (string phone, bool me, [NullAllowed] string username);

		// -(BOOL)cancelDialOut:(BOOL)isCallMe;
		[Export ("cancelDialOut:")]
		bool CancelDialOut (bool isCallMe);

		// -(MobileRTCCallCountryCode * _Nullable)getDialInCurrentCountryCode;
		[Export ("getDialInCurrentCountryCode")]
		MobileRTCCallCountryCode DialInCurrentCountryCode();

		//// -(NSArray<NSArray<MobileRTCCallCountryCode *> *> * _Nullable)getDialInAllCountryCodes;
		//[NullAllowed, Export ("getDialInAllCountryCodes")]
		//		//NSArray<MobileRTCCallCountryCode>[] DialInAllCountryCodes { get; }

		// -(NSArray<MobileRTCCallCountryCode *> * _Nullable)getDialInCallCodesWithCountryId:(NSString * _Nullable)countryId;
		[Export ("getDialInCallCodesWithCountryId:")]
		[return: NullAllowed]
		MobileRTCCallCountryCode[] GetDialInCallCodesWithCountryId ([NullAllowed] string countryId);

		// -(BOOL)dialInCall:(NSString * _Nullable)countryNumber;
		[Export ("dialInCall:")]
		bool DialInCall ([NullAllowed] string countryNumber);
	}

	// @interface MobileRTCMeetingSettings : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCMeetingSettings
	{
		// @property (assign, nonatomic) BOOL meetingTitleHidden;
		[Export ("meetingTitleHidden")]
		bool MeetingTitleHidden { get; set; }

		// @property (assign, nonatomic) BOOL meetingPasswordHidden;
		[Export ("meetingPasswordHidden")]
		bool MeetingPasswordHidden { get; set; }

		// @property (assign, nonatomic) BOOL meetingLeaveHidden;
		[Export ("meetingLeaveHidden")]
		bool MeetingLeaveHidden { get; set; }

		// @property (assign, nonatomic) BOOL meetingAudioHidden;
		[Export ("meetingAudioHidden")]
		bool MeetingAudioHidden { get; set; }

		// @property (assign, nonatomic) BOOL meetingVideoHidden;
		[Export ("meetingVideoHidden")]
		bool MeetingVideoHidden { get; set; }

		// @property (assign, nonatomic) BOOL meetingInviteHidden;
		[Export ("meetingInviteHidden")]
		bool MeetingInviteHidden { get; set; }

		// @property (assign, nonatomic) BOOL meetingInviteUrlHidden;
		[Export ("meetingInviteUrlHidden")]
		bool MeetingInviteUrlHidden { get; set; }

		// @property (assign, nonatomic) BOOL meetingChatHidden;
		[Export ("meetingChatHidden")]
		bool MeetingChatHidden { get; set; }

		// @property (assign, nonatomic) BOOL meetingParticipantHidden;
		[Export ("meetingParticipantHidden")]
		bool MeetingParticipantHidden { get; set; }

		// @property (assign, nonatomic) BOOL meetingShareHidden;
		[Export ("meetingShareHidden")]
		bool MeetingShareHidden { get; set; }

		// @property (assign, nonatomic) BOOL meetingMoreHidden;
		[Export ("meetingMoreHidden")]
		bool MeetingMoreHidden { get; set; }

		// @property (assign, nonatomic) BOOL topBarHidden;
		[Export ("topBarHidden")]
		bool TopBarHidden { get; set; }

		// @property (assign, nonatomic) BOOL bottomBarHidden;
		[Export ("bottomBarHidden")]
		bool BottomBarHidden { get; set; }

		// @property (assign, nonatomic) BOOL disconnectAudioHidden;
		[Export ("disconnectAudioHidden")]
		bool DisconnectAudioHidden { get; set; }

		// @property (assign, nonatomic) BOOL recordButtonHidden;
		[Export ("recordButtonHidden")]
		bool RecordButtonHidden { get; set; }

		// @property (assign, nonatomic) BOOL thumbnailInShare;
		[Export ("thumbnailInShare")]
		bool ThumbnailInShare { get; set; }

		// @property (assign, nonatomic) BOOL hostLeaveHidden;
		[Export ("hostLeaveHidden")]
		bool HostLeaveHidden { get; set; }

		// @property (assign, nonatomic) BOOL hintHidden;
		[Export ("hintHidden")]
		bool HintHidden { get; set; }

		// @property (assign, nonatomic) BOOL waitingHUDHidden;
		[Export ("waitingHUDHidden")]
		bool WaitingHUDHidden { get; set; }

		// @property (assign, nonatomic) BOOL callinRoomSystemHidden;
		[Export ("callinRoomSystemHidden")]
		bool CallinRoomSystemHidden { get; set; }

		// @property (assign, nonatomic) BOOL calloutRoomSystemHidden;
		[Export ("calloutRoomSystemHidden")]
		bool CalloutRoomSystemHidden { get; set; }

		// @property (assign, nonatomic) BOOL claimHostWithHostKeyHidden;
		[Export ("claimHostWithHostKeyHidden")]
		bool ClaimHostWithHostKeyHidden { get; set; }

		// @property (assign, nonatomic) BOOL closeCaptionHidden;
		[Export ("closeCaptionHidden")]
		bool CloseCaptionHidden { get; set; }

		// @property (assign, nonatomic) BOOL qaButtonHidden;
		[Export ("qaButtonHidden")]
		bool QaButtonHidden { get; set; }

		// @property (assign, nonatomic) BOOL promoteToPanelistHidden;
		[Export ("promoteToPanelistHidden")]
		bool PromoteToPanelistHidden { get; set; }

		// @property (assign, nonatomic) BOOL changeToAttendeeHidden;
		[Export ("changeToAttendeeHidden")]
		bool ChangeToAttendeeHidden { get; set; }

		// @property (assign, nonatomic) BOOL proximityMonitoringDisable;
		[Export ("proximityMonitoringDisable")]
		bool ProximityMonitoringDisable { get; set; }

		// @property (assign, nonatomic) BOOL hideFeedbackButtonOnCloudWhiteboard;
		[Export ("hideFeedbackButtonOnCloudWhiteboard")]
		bool HideFeedbackButtonOnCloudWhiteboard { get; set; }

		// @property (assign, nonatomic) BOOL hideShareButtonOnCloudWhiteboard;
		[Export ("hideShareButtonOnCloudWhiteboard")]
		bool HideShareButtonOnCloudWhiteboard { get; set; }

		// @property (assign, nonatomic) BOOL hideAboutButtonOnCloudWhiteboard;
		[Export ("hideAboutButtonOnCloudWhiteboard")]
		bool HideAboutButtonOnCloudWhiteboard { get; set; }

		// -(BOOL)autoConnectInternetAudio;
		// -(void)setAutoConnectInternetAudio:(BOOL)connected;
		[Export ("autoConnectInternetAudio")]
		bool AutoConnectInternetAudio { get; set; }

		// -(BOOL)muteAudioWhenJoinMeeting;
		[Export ("muteAudioWhenJoinMeeting")]
		bool MuteAudioWhenJoinMeeting ();

		// -(void)setMuteAudioWhenJoinMeeting:(BOOL)muted;
		[Export ("setMuteAudioWhenJoinMeeting:")]
		void SetMuteAudioWhenJoinMeeting (bool muted);

		// -(BOOL)muteVideoWhenJoinMeeting;
		[Export ("muteVideoWhenJoinMeeting")]
		bool MuteVideoWhenJoinMeeting ();

		// -(void)setMuteVideoWhenJoinMeeting:(BOOL)muted;
		[Export ("setMuteVideoWhenJoinMeeting:")]
		void SetMuteVideoWhenJoinMeeting (bool muted);

		// -(BOOL)faceBeautyEnabled;
		[Export ("faceBeautyEnabled")]
		bool FaceBeautyEnabled ();

		// -(void)setFaceBeautyEnabled:(BOOL)enable;
		[Export ("setFaceBeautyEnabled:")]
		void SetFaceBeautyEnabled (bool enable);

		// -(BOOL)isMirrorEffectEnabled;
		[Export ("isMirrorEffectEnabled")]
		bool IsMirrorEffectEnabled ();

		// -(void)enableMirrorEffect:(BOOL)enable;
		[Export ("enableMirrorEffect:")]
		void EnableMirrorEffect (bool enable);

		// -(BOOL)driveModeDisabled;
		[Export ("driveModeDisabled")]
		bool DriveModeDisabled ();

		// -(void)disableDriveMode:(BOOL)disabled;
		[Export ("disableDriveMode:")]
		void DisableDriveMode (bool disabled);

		// -(BOOL)galleryViewDisabled;
		[Export ("galleryViewDisabled")]
		bool GalleryViewDisabled ();

		// -(void)disableGalleryView:(BOOL)disabled;
		[Export ("disableGalleryView:")]
		void DisableGalleryView (bool disabled);

		// -(void)disableCloudWhiteboard:(BOOL)disabled;
		[Export ("disableCloudWhiteboard:")]
		void DisableCloudWhiteboard (bool disabled);

		// -(BOOL)callInDisabled;
		[Export ("callInDisabled")]
		bool CallInDisabled ();

		// -(void)disableCallIn:(BOOL)disabled;
		[Export ("disableCallIn:")]
		void DisableCallIn (bool disabled);

		// -(BOOL)callOutDisabled;
		[Export ("callOutDisabled")]
		bool CallOutDisabled ();

		// -(void)disableCallOut:(BOOL)disabled;
		[Export ("disableCallOut:")]
		void DisableCallOut (bool disabled);

		// -(BOOL)minimizeMeetingDisabled;
		[Export ("minimizeMeetingDisabled")]
		bool MinimizeMeetingDisabled ();

		// -(void)disableMinimizeMeeting:(BOOL)disabled;
		[Export ("disableMinimizeMeeting:")]
		void DisableMinimizeMeeting (bool disabled);

		// -(BOOL)freeMeetingUpgradeTipsDisabled;
		[Export ("freeMeetingUpgradeTipsDisabled")]
		bool FreeMeetingUpgradeTipsDisabled ();

		// -(void)disableFreeMeetingUpgradeTips:(BOOL)disabled;
		[Export ("disableFreeMeetingUpgradeTips:")]
		void DisableFreeMeetingUpgradeTips (bool disabled);

		// -(BOOL)speakerOffWhenInMeeting;
		[Export ("speakerOffWhenInMeeting")]
		bool SpeakerOffWhenInMeeting ();

		// -(void)setSpeakerOffWhenInMeeting:(BOOL)speakerOff;
		[Export ("setSpeakerOffWhenInMeeting:")]
		void SetSpeakerOffWhenInMeeting (bool speakerOff);

		// -(BOOL)showMyMeetingElapseTime;
		[Export ("showMyMeetingElapseTime")]
		bool ShowMyMeetingElapseTime ();

		// -(void)enableShowMyMeetingElapseTime:(BOOL)enable;
		[Export ("enableShowMyMeetingElapseTime:")]
		void EnableShowMyMeetingElapseTime (bool enable);

		// -(BOOL)micOriginalInputEnabled;
		[Export ("micOriginalInputEnabled")]
		bool MicOriginalInputEnabled ();

		// -(void)enableMicOriginalInput:(BOOL)enable;
		[Export ("enableMicOriginalInput:")]
		void EnableMicOriginalInput (bool enable);

		// -(BOOL)reactionsOnMeetingUIHidden;
		[Export ("reactionsOnMeetingUIHidden")]
		bool ReactionsOnMeetingUIHidden ();

		// -(void)hideReactionsOnMeetingUI:(BOOL)hidden;
		[Export ("hideReactionsOnMeetingUI:")]
		void HideReactionsOnMeetingUI (bool hidden);

		// -(BOOL)showVideoPreviewWhenJoinMeetingDisabled;
		[Export ("showVideoPreviewWhenJoinMeetingDisabled")]
		bool ShowVideoPreviewWhenJoinMeetingDisabled ();

		// -(void)disableShowVideoPreviewWhenJoinMeeting:(BOOL)disabled;
		[Export ("disableShowVideoPreviewWhenJoinMeeting:")]
		void DisableShowVideoPreviewWhenJoinMeeting (bool disabled);

		// -(BOOL)virtualBackgroundDisabled;
		[Export ("virtualBackgroundDisabled")]
		bool VirtualBackgroundDisabled ();

		// -(void)disableVirtualBackground:(BOOL)disabled;
		[Export ("disableVirtualBackground:")]
		void DisableVirtualBackground (bool disabled);

		// -(void)prePopulateWebinarRegistrationInfo:(NSString * _Nonnull)email username:(NSString * _Nonnull)username;
		[Export ("prePopulateWebinarRegistrationInfo:username:")]
		void PrePopulateWebinarRegistrationInfo (string email, string username);

		// -(void)setHideRegisterWebinarInfoWindow:(BOOL)hide;
		[Export ("setHideRegisterWebinarInfoWindow:")]
		void SetHideRegisterWebinarInfoWindow (bool hide);

		// -(BOOL)hideRegisterWebinarInfoWindow;
		[Export ("hideRegisterWebinarInfoWindow")]
		bool HideRegisterWebinarInfoWindow ();

		// -(BOOL)disableConfidentialWatermark:(BOOL)disable;
		[Export ("disableConfidentialWatermark:")]
		bool DisableConfidentialWatermark (bool disable);

		// -(BOOL)copyMeetingUrlDisabled;
		[Export ("copyMeetingUrlDisabled")]
		bool CopyMeetingUrlDisabled ();

		// -(void)disableCopyMeetingUrl:(BOOL)disabled;
		[Export ("disableCopyMeetingUrl:")]
		void DisableCopyMeetingUrl (bool disabled);

		// -(MobileRTCMeetError)setReactionSkinTone:(MobileRTCEmojiReactionSkinTone)skinTone;
		[Export ("setReactionSkinTone:")]
		MobileRTCMeetError SetReactionSkinTone (MobileRTCEmojiReactionSkinTone skinTone);

		// -(MobileRTCEmojiReactionSkinTone)reactionSkinTone;
		[Export ("reactionSkinTone")]
		MobileRTCEmojiReactionSkinTone ReactionSkinTone ();

		// -(void)disableClearWebKitCache:(BOOL)disabled;
		[Export ("disableClearWebKitCache:")]
		void DisableClearWebKitCache (bool disabled);

		// -(BOOL)isDisabledClearWebKitCache;
		[Export ("isDisabledClearWebKitCache")]
		bool IsDisabledClearWebKitCache ();

		// -(BOOL)isHideNoVideoUsersEnabled;
		[Export ("isHideNoVideoUsersEnabled")]
		bool IsHideNoVideoUsersEnabled ();

		// -(void)setHideNoVideoUsersEnabled:(BOOL)enabled;
		[Export ("setHideNoVideoUsersEnabled:")]
		void SetHideNoVideoUsersEnabled (bool enabled);

		// -(void)enableHideSelfView:(BOOL)isHidden;
		[Export ("enableHideSelfView:")]
		void EnableHideSelfView (bool isHidden);

		// -(BOOL)isHideSelfViewEnabled;
		[Export ("isHideSelfViewEnabled")]
		bool IsHideSelfViewEnabled ();

		// -(void)hideRequestRecordPrivilegeDialog:(BOOL)bHide;
		[Export ("hideRequestRecordPrivilegeDialog:")]
		void HideRequestRecordPrivilegeDialog (bool bHide);

		// -(BOOL)videoCallPictureInPictureEnabled;
		[Export ("videoCallPictureInPictureEnabled")]
		bool VideoCallPictureInPictureEnabled ();

		// -(void)enableVideoCallPictureInPicture:(BOOL)enable;
		[Export ("enableVideoCallPictureInPicture:")]
		void EnableVideoCallPictureInPicture (bool enable);
	}

	// @interface MobileRTCInviteHelper : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCInviteHelper
	{
		// @property (readonly, retain, nonatomic) NSString * _Nonnull ongoingMeetingNumber;
		[Export ("ongoingMeetingNumber", ArgumentSemantic.Retain)]
		string OngoingMeetingNumber { get; }

		// @property (readonly, retain, nonatomic) NSString * _Nonnull ongoingMeetingID;
		[Export ("ongoingMeetingID", ArgumentSemantic.Retain)]
		string OngoingMeetingID { get; }

		// @property (readonly, retain, nonatomic) NSString * _Nonnull ongoingMeetingTopic;
		[Export ("ongoingMeetingTopic", ArgumentSemantic.Retain)]
		string OngoingMeetingTopic { get; }

		// @property (readonly, retain, nonatomic) NSDate * _Nonnull ongoingMeetingStartTime;
		[Export ("ongoingMeetingStartTime", ArgumentSemantic.Retain)]
		NSDate OngoingMeetingStartTime { get; }

		// @property (readonly, assign, nonatomic) BOOL ongoingRecurringMeeting;
		[Export ("ongoingRecurringMeeting")]
		bool OngoingRecurringMeeting { get; }

		// @property (readonly, retain, nonatomic) NSString * _Nonnull joinMeetingURL;
		[Export ("joinMeetingURL", ArgumentSemantic.Retain)]
		string JoinMeetingURL { get; }

		// @property (readonly, retain, nonatomic) NSString * _Nonnull meetingPassword;
		[Export ("meetingPassword", ArgumentSemantic.Retain)]
		string MeetingPassword { get; }

		// @property (readonly, retain, nonatomic) NSString * _Nonnull rawMeetingPassword;
		[Export ("rawMeetingPassword", ArgumentSemantic.Retain)]
		string RawMeetingPassword { get; }

		// @property (readonly, retain, nonatomic) NSString * _Nonnull tollCallInNumber;
		[Export ("tollCallInNumber", ArgumentSemantic.Retain)]
		string TollCallInNumber { get; }

		// @property (readonly, retain, nonatomic) NSString * _Nonnull tollFreeCallInNumber;
		[Export ("tollFreeCallInNumber", ArgumentSemantic.Retain)]
		string TollFreeCallInNumber { get; }

		// @property (assign, nonatomic) BOOL disableInviteSMS;
		[Export ("disableInviteSMS")]
		bool DisableInviteSMS { get; set; }

		// @property (retain, nonatomic) NSString * _Nonnull inviteSMS;
		[Export ("inviteSMS", ArgumentSemantic.Retain)]
		string InviteSMS { get; set; }

		// @property (assign, nonatomic) BOOL disableCopyURL;
		[Export ("disableCopyURL")]
		bool DisableCopyURL { get; set; }

		// @property (retain, nonatomic) NSString * _Nonnull inviteCopyURL;
		[Export ("inviteCopyURL", ArgumentSemantic.Retain)]
		string InviteCopyURL { get; set; }

		// @property (assign, nonatomic) BOOL disableInviteEmail;
		[Export ("disableInviteEmail")]
		bool DisableInviteEmail { get; set; }

		// @property (retain, nonatomic) NSString * _Nullable inviteEmailSubject;
		[NullAllowed, Export ("inviteEmailSubject", ArgumentSemantic.Retain)]
		string InviteEmailSubject { get; set; }

		// @property (retain, nonatomic) NSString * _Nullable inviteEmailContent;
		[NullAllowed, Export ("inviteEmailContent", ArgumentSemantic.Retain)]
		string InviteEmailContent { get; set; }

		// +(MobileRTCInviteHelper * _Nonnull)sharedInstance;
		[Static]
		[Export ("sharedInstance")]
		MobileRTCInviteHelper SharedInstance { get; }
	}

	// @interface MobileRTCVideoView : UIView
	[BaseType (typeof(UIView))]
	interface MobileRTCVideoView
	{
		// -(NSInteger)getUserID;
		[Export ("getUserID")]
		nint UserID { get; }

		// -(BOOL)showAttendeeVideoWithUserID:(NSUInteger)userID;
		[Export ("showAttendeeVideoWithUserID:")]
		bool ShowAttendeeVideoWithUserID (nuint userID);

		// -(void)stopAttendeeVideo;
		[Export ("stopAttendeeVideo")]
		void StopAttendeeVideo ();

		// -(void)setVideoAspect:(MobileRTCVideoAspect)aspect;
		[Export ("setVideoAspect:")]
		void SetVideoAspect (MobileRTCVideoAspect aspect);
	}

	// @interface MobileRTCPreviewVideoView : MobileRTCVideoView
	[BaseType (typeof(MobileRTCVideoView))]
	interface MobileRTCPreviewVideoView
	{
	}

	// @interface MobileRTCActiveVideoView : MobileRTCVideoView
	[BaseType (typeof(MobileRTCVideoView))]
	interface MobileRTCActiveVideoView
	{
	}

	// @interface MobileRTCActiveShareView : MobileRTCVideoView
	[BaseType (typeof(MobileRTCVideoView))]
	interface MobileRTCActiveShareView
	{
		// -(void)showActiveShareWithUserID:(NSUInteger)userID;
		[Export ("showActiveShareWithUserID:")]
		void ShowActiveShareWithUserID (nuint userID);

		// -(void)stopActiveShare;
		[Export ("stopActiveShare")]
		void StopActiveShare ();

		// -(void)changeShareScaleWithUserID:(NSUInteger)userID;
		[Export ("changeShareScaleWithUserID:")]
		void ChangeShareScaleWithUserID (nuint userID);
	}

	// typedef void (^MobileRTCMeetingInviteActionItemBlock)();
	delegate void MobileRTCMeetingInviteActionItemBlock ();

	// @interface MobileRTCMeetingInviteActionItem : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCMeetingInviteActionItem
	{
		// @property (readwrite, retain, nonatomic) NSString * _Nonnull actionTitle;
		[Export ("actionTitle", ArgumentSemantic.Retain)]
		string ActionTitle { get; set; }

		// @property (readwrite, copy, nonatomic) MobileRTCMeetingInviteActionItemBlock _Nonnull actionHandler;
		[Export ("actionHandler", ArgumentSemantic.Copy)]
		MobileRTCMeetingInviteActionItemBlock ActionHandler { get; set; }

		// +(id _Nonnull)itemWithTitle:(NSString * _Nonnull)inTitle Action:(MobileRTCMeetingInviteActionItemBlock _Nonnull)actionHandler;
		[Static]
		[Export ("itemWithTitle:Action:")]
		NSObject ItemWithTitle (string inTitle, MobileRTCMeetingInviteActionItemBlock actionHandler);
	}

	// @protocol MobileRTCMeetingShareActionItemDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface MobileRTCMeetingShareActionItemDelegate
	{
		// @required -(void)onShareItemClicked:(NSUInteger)tag completion:(BOOL (^ _Nonnull)(UIViewController * _Nonnull))completion;
		[Abstract]
		[Export ("onShareItemClicked:completion:")]
		void Completion (nuint tag, Func<UIViewController, bool> completion);
	}

	// @interface MobileRTCMeetingShareActionItem : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCMeetingShareActionItem
	{
		// @property (readwrite, retain, nonatomic) NSString * _Nonnull actionTitle;
		[Export ("actionTitle", ArgumentSemantic.Retain)]
		string ActionTitle { get; set; }

		// @property (assign, readwrite, nonatomic) NSUInteger tag;
		[Export ("tag")]
		nuint Tag { get; set; }

		[Wrap ("WeakDelegate")]
		MobileRTCMeetingShareActionItemDelegate Delegate { get; set; }

		// @property (assign, readwrite, nonatomic) id<MobileRTCMeetingShareActionItemDelegate> _Nonnull delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Assign)]
		NSObject WeakDelegate { get; set; }

		// +(id _Nonnull)itemWithTitle:(NSString * _Nonnull)inTitle Tag:(NSUInteger)tag;
		[Static]
		[Export ("itemWithTitle:Tag:")]
		NSObject ItemWithTitle (string inTitle, nuint tag);
	}

	// @protocol MobileRTCAnnotationServiceDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface MobileRTCAnnotationServiceDelegate
	{
		// @optional -(void)onAnnotationService:(MobileRTCAnnotationService * _Nullable)service supportStatusChanged:(BOOL)support;
		[Export ("onAnnotationService:supportStatusChanged:")]
		void SupportStatusChanged ([NullAllowed] MobileRTCAnnotationService service, bool support);
	}

	// @interface MobileRTCAnnotationService : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCAnnotationService
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		MobileRTCAnnotationServiceDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<MobileRTCAnnotationServiceDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(MobileRTCAnnotationError)startAnnotationWithSharedView:(UIView * _Nullable)view;
		[Export ("startAnnotationWithSharedView:")]
		MobileRTCAnnotationError StartAnnotationWithSharedView ([NullAllowed] UIView view);

		// -(BOOL)stopAnnotation;
		[Export ("stopAnnotation")]
		bool StopAnnotation { get; }

		// -(MobileRTCAnnotationError)setToolColor:(UIColor * _Nullable)toolColor;
		[Export ("setToolColor:")]
		MobileRTCAnnotationError SetToolColor ([NullAllowed] UIColor toolColor);

		// -(UIColor * _Nullable)getToolColor;
		[NullAllowed, Export ("getToolColor")]
		UIColor ToolColor { get; }

		// -(MobileRTCAnnotationError)setToolType:(MobileRTCAnnoTool)type;
		[Export ("setToolType:")]
		MobileRTCAnnotationError SetToolType (MobileRTCAnnoTool type);

		// -(MobileRTCAnnoTool)getToolType;
		[Export ("getToolType")]
		MobileRTCAnnoTool ToolType { get; }

		// -(MobileRTCAnnotationError)setToolWidth:(NSUInteger)width;
		[Export ("setToolWidth:")]
		MobileRTCAnnotationError SetToolWidth (nuint width);

		// -(NSUInteger)getToolWidth;
		[Export ("getToolWidth")]
		nuint ToolWidth { get; }

		// -(MobileRTCAnnotationError)clear:(MobileRTCAnnoClearType)type;
		[Export ("clear:")]
		MobileRTCAnnotationError Clear (MobileRTCAnnoClearType type);

		// -(MobileRTCAnnotationError)undo;
		[Export ("undo")]
		MobileRTCAnnotationError Undo { get; }

		// -(MobileRTCAnnotationError)redo;
		[Export ("redo")]
		MobileRTCAnnotationError Redo { get; }

		// -(BOOL)isPresenter;
		[Export ("isPresenter")]
		bool IsPresenter { get; }

		// -(BOOL)canDisableViewerAnnoataion;
		[Export ("canDisableViewerAnnoataion")]
		bool CanDisableViewerAnnoataion { get; }

		// -(BOOL)isViewerAnnoataionDisabled;
		[Export ("isViewerAnnoataionDisabled")]
		bool IsViewerAnnoataionDisabled { get; }

		// -(MobileRTCAnnotationError)disableViewerAnnoataion:(BOOL)isDisable;
		[Export ("disableViewerAnnoataion:")]
		MobileRTCAnnotationError DisableViewerAnnoataion (bool isDisable);

		// -(BOOL)canDoAnnotation;
		[Export ("canDoAnnotation")]
		bool CanDoAnnotation { get; }

		// -(BOOL)isAnnotationLegalNoticeAvailable;
		[Export ("isAnnotationLegalNoticeAvailable")]
		bool IsAnnotationLegalNoticeAvailable { get; }

		// -(NSString * _Nullable)getAnnotationLegalNoticesPrompt;
		[NullAllowed, Export ("getAnnotationLegalNoticesPrompt")]
		string AnnotationLegalNoticesPrompt { get; }

		// -(NSString * _Nullable)getAnnotationLegalNoticesExplained;
		[NullAllowed, Export ("getAnnotationLegalNoticesExplained")]
		string AnnotationLegalNoticesExplained { get; }
	}

	// @interface MobileRTCRemoteControlService : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCRemoteControlService
	{
		[Wrap ("WeakDelegate")]
		MobileRTCRemoteControlDelegate Delegate { get; set; }

		// @property (assign, nonatomic) id<MobileRTCRemoteControlDelegate> _Nonnull delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Assign)]
		NSObject WeakDelegate { get; set; }

		// -(BOOL)isHaveRemoteControlRight;
		[Export ("isHaveRemoteControlRight")]
		bool IsHaveRemoteControlRight { get; }

		// -(BOOL)isRemoteController;
		[Export ("isRemoteController")]
		bool IsRemoteController { get; }

		// -(MobileRTCRemoteControlError)grabRemoteControl:(UIView * _Nonnull)remoteShareView;
		[Export ("grabRemoteControl:")]
		MobileRTCRemoteControlError GrabRemoteControl (UIView remoteShareView);

		// -(MobileRTCRemoteControlError)remoteControlSingleTap:(CGPoint)point;
		[Export ("remoteControlSingleTap:")]
		MobileRTCRemoteControlError RemoteControlSingleTap (CGPoint point);

		// -(MobileRTCRemoteControlError)remoteControlDoubleTap:(CGPoint)point;
		[Export ("remoteControlDoubleTap:")]
		MobileRTCRemoteControlError RemoteControlDoubleTap (CGPoint point);

		// -(MobileRTCRemoteControlError)remoteControlLongPress:(CGPoint)point;
		[Export ("remoteControlLongPress:")]
		MobileRTCRemoteControlError RemoteControlLongPress (CGPoint point);

		// -(MobileRTCRemoteControlError)remoteControlDoubleScroll:(CGPoint)point;
		[Export ("remoteControlDoubleScroll:")]
		MobileRTCRemoteControlError RemoteControlDoubleScroll (CGPoint point);

		// -(MobileRTCRemoteControlError)remoteControlSingleMove:(CGPoint)point;
		[Export ("remoteControlSingleMove:")]
		MobileRTCRemoteControlError RemoteControlSingleMove (CGPoint point);

		// -(MobileRTCRemoteControlError)remoteControlMouseLeftDown:(CGPoint)point;
		[Export ("remoteControlMouseLeftDown:")]
		MobileRTCRemoteControlError RemoteControlMouseLeftDown (CGPoint point);

		// -(MobileRTCRemoteControlError)remoteControlMouseLeftUp:(CGPoint)point;
		[Export ("remoteControlMouseLeftUp:")]
		MobileRTCRemoteControlError RemoteControlMouseLeftUp (CGPoint point);

		// -(MobileRTCRemoteControlError)remoteControlMouseLeftDrag:(CGPoint)point;
		[Export ("remoteControlMouseLeftDrag:")]
		MobileRTCRemoteControlError RemoteControlMouseLeftDrag (CGPoint point);

		// -(MobileRTCRemoteControlError)remoteControlCharInput:(NSString * _Nonnull)str;
		[Export ("remoteControlCharInput:")]
		MobileRTCRemoteControlError RemoteControlCharInput (string str);

		// -(MobileRTCRemoteControlError)remoteControlKeyInput:(MobileRTCRemoteControlInputType)key;
		[Export ("remoteControlKeyInput:")]
		MobileRTCRemoteControlError RemoteControlKeyInput (MobileRTCRemoteControlInputType key);
	}

	// @protocol MobileRTCRemoteControlDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface MobileRTCRemoteControlDelegate
	{
		// @optional -(void)remoteControlPrivilegeChanged:(BOOL)isMyControl;
		[Export ("remoteControlPrivilegeChanged:")]
		void RemoteControlPrivilegeChanged (bool isMyControl);

		// @optional -(void)startRemoteControlCallBack:(MobileRTCRemoteControlError)resultValue;
		[Export ("startRemoteControlCallBack:")]
		void StartRemoteControlCallBack (MobileRTCRemoteControlError resultValue);
	}

	//// @interface MobileRTCCustomWaitingRoomData : NSObject
	//[BaseType (typeof(NSObject))]
	//interface MobileRTCCustomWaitingRoomData
	//{
	//	// @property (retain, nonatomic) NSString * _Nullable title;
	//	[NullAllowed, Export ("title", ArgumentSemantic.Retain)]
	//	string Title { get; set; }

	//	// @property (retain, nonatomic) NSString * _Nullable descriptionString;
	//	[NullAllowed, Export ("descriptionString", ArgumentSemantic.Retain)]
	//	string DescriptionString { get; set; }

	//	// @property (retain, nonatomic) NSString * _Nullable logoPath;
	//	[NullAllowed, Export ("logoPath", ArgumentSemantic.Retain)]
	//	string LogoPath { get; set; }

	//	// @property (retain, nonatomic) NSString * _Nullable videoPath;
	//	[NullAllowed, Export ("videoPath", ArgumentSemantic.Retain)]
	//	string VideoPath { get; set; }

	//	// @property (assign, nonatomic) MobileRTCWaitingRoomLayoutType type;
	//	[Export ("type", ArgumentSemantic.Assign)]
	//	MobileRTCWaitingRoomLayoutType Type { get; set; }

	//	// @property (assign, nonatomic) MobileRTCCustomWaitingRoomDataStatus status;
	//	[Export ("status", ArgumentSemantic.Assign)]
	//	MobileRTCCustomWaitingRoomDataStatus Status { get; set; }
	//}

	// @protocol MobileRTCWaitingRoomServiceDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface MobileRTCWaitingRoomServiceDelegate
	{
		// @optional -(void)onWaitingRoomUserJoin:(NSUInteger)userId;
		[Export ("onWaitingRoomUserJoin:")]
		void OnWaitingRoomUserJoin (nuint userId);

		// @optional -(void)onWaitingRoomUserLeft:(NSUInteger)userId;
		[Export ("onWaitingRoomUserLeft:")]
		void OnWaitingRoomUserLeft (nuint userId);

		// @optional -(void)onWaitingRoomPresetAudioStatusChanged:(BOOL)audioCanTurnOn;
		[Export ("onWaitingRoomPresetAudioStatusChanged:")]
		void OnWaitingRoomPresetAudioStatusChanged (bool audioCanTurnOn);

		// @optional -(void)onWaitingRoomPresetVideoStatusChanged:(BOOL)videoCanTurnOn;
		[Export ("onWaitingRoomPresetVideoStatusChanged:")]
		void OnWaitingRoomPresetVideoStatusChanged (bool videoCanTurnOn);

		//// @optional -(void)onCustomWaitingRoomDataUpdated:(MobileRTCCustomWaitingRoomData * _Nullable)data;
		//[Export ("onCustomWaitingRoomDataUpdated:")]
		//void OnCustomWaitingRoomDataUpdated ([NullAllowed] MobileRTCCustomWaitingRoomData data);

		// @optional -(void)onWaitingRoomUserNameChanged:(NSInteger)userID userName:(NSString * _Nonnull)userName;
		[Export ("onWaitingRoomUserNameChanged:userName:")]
		void OnWaitingRoomUserNameChanged (nint userID, string userName);
	}

	// @interface MobileRTCWaitingRoomService : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCWaitingRoomService
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		MobileRTCWaitingRoomServiceDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<MobileRTCWaitingRoomServiceDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(BOOL)isSupportWaitingRoom;
		[Export ("isSupportWaitingRoom")]
		bool IsSupportWaitingRoom { get; }

		// -(BOOL)isWaitingRoomOnEntryFlagOn;
		[Export ("isWaitingRoomOnEntryFlagOn")]
		bool IsWaitingRoomOnEntryFlagOn { get; }

		// -(MobileRTCMeetError)enableWaitingRoomOnEntry:(BOOL)bEnable;
		[Export ("enableWaitingRoomOnEntry:")]
		MobileRTCMeetError EnableWaitingRoomOnEntry (bool bEnable);

		// -(NSArray<NSNumber *> * _Nullable)waitingRoomList;
		[NullAllowed, Export ("waitingRoomList")]
		NSNumber[] WaitingRoomList { get; }

		// -(MobileRTCMeetingUserInfo * _Nullable)waitingRoomUserInfoByID:(NSUInteger)userId;
		[Export ("waitingRoomUserInfoByID:")]
		[return: NullAllowed]
		MobileRTCMeetingUserInfo WaitingRoomUserInfoByID (nuint userId);

		// -(MobileRTCSDKError)admitToMeeting:(NSUInteger)userId;
		[Export ("admitToMeeting:")]
		MobileRTCSDKError AdmitToMeeting (nuint userId);

		// -(MobileRTCSDKError)admitAllToMeeting;
		[Export ("admitAllToMeeting")]
		MobileRTCSDKError AdmitAllToMeeting { get; }

		// -(MobileRTCSDKError)putInWaitingRoom:(NSUInteger)userId;
		[Export ("putInWaitingRoom:")]
		MobileRTCSDKError PutInWaitingRoom (nuint userId);

		// -(BOOL)isAudioEnabledInWaitingRoom;
		[Export ("isAudioEnabledInWaitingRoom")]
		bool IsAudioEnabledInWaitingRoom { get; }

		// -(BOOL)isVideoEnabledInWaitingRoom;
		[Export ("isVideoEnabledInWaitingRoom")]
		bool IsVideoEnabledInWaitingRoom { get; }

		// -(MobileRTCSDKError)requestCustomWaitingRoomData;
		[Export ("requestCustomWaitingRoomData")]
		MobileRTCSDKError RequestCustomWaitingRoomData { get; }

		// -(BOOL)canRenameUser;
		[Export ("canRenameUser")]
		bool CanRenameUser { get; }

		// -(MobileRTCSDKError)renameUser:(NSInteger)userID newUserName:(NSString * _Nonnull)userName;
		[Export ("renameUser:newUserName:")]
		MobileRTCSDKError RenameUser (nint userID, string userName);

		// -(BOOL)canExpelUser;
		[Export ("canExpelUser")]
		bool CanExpelUser { get; }

		// -(MobileRTCSDKError)expelUser:(NSInteger)userID;
		[Export ("expelUser:")]
		MobileRTCSDKError ExpelUser (nint userID);
	}

	// @interface MobileRTCRenderer : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCRenderer
	{
		// @property (readonly, assign, nonatomic) NSUInteger userId;
		[Export ("userId")]
		nuint UserId { get; }

		// @property (readonly, assign, nonatomic) MobileRTCVideoType videoType;
		[Export ("videoType", ArgumentSemantic.Assign)]
		MobileRTCVideoType VideoType { get; }

		// @property (readonly, assign, nonatomic) MobileRTCVideoResolution resolution;
		[Export ("resolution", ArgumentSemantic.Assign)]
		MobileRTCVideoResolution Resolution { get; }

		// -(instancetype _Nonnull)initWithDelegate:(id<MobileRTCVideoRawDataDelegate> _Nonnull)delegate;
		[Export ("initWithDelegate:")]
		System.IntPtr Constructor (MobileRTCVideoRawDataDelegate @delegate);

		// -(MobileRTCRawDataError)setRawDataResolution:(MobileRTCVideoResolution)resolution;
		[Export ("setRawDataResolution:")]
		MobileRTCRawDataError SetRawDataResolution (MobileRTCVideoResolution resolution);

		// -(MobileRTCRawDataError)subscribe:(NSUInteger)userId videoType:(MobileRTCVideoType)type;
		[Export ("subscribe:videoType:")]
		MobileRTCRawDataError Subscribe (nuint userId, MobileRTCVideoType type);

		// -(MobileRTCRawDataError)unSubscribe;
		[Export ("unSubscribe")]
		MobileRTCRawDataError UnSubscribe { get; }
	}

	// @interface MobileRTCAudioRawDataHelper : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCAudioRawDataHelper
	{
		// -(instancetype _Nonnull)initWithDelegate:(id<MobileRTCAudioRawDataDelegate> _Nonnull)delegate;
		[Export ("initWithDelegate:")]
		System.IntPtr Constructor (MobileRTCAudioRawDataDelegate @delegate);

		// -(MobileRTCRawDataError)subscribe;
		[Export ("subscribe")]
		MobileRTCRawDataError Subscribe { get; }

		// -(MobileRTCRawDataError)unSubscribe;
		[Export ("unSubscribe")]
		MobileRTCRawDataError UnSubscribe { get; }
	}

	// @interface MobileRTCVideoSourceHelper : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCVideoSourceHelper
	{
		// -(MobileRTCRawDataError)setPreProcessor:(id<MobileRTCPreProcessorDelegate>)delegate;
		[Export ("setPreProcessor:")]
		MobileRTCRawDataError SetPreProcessor (MobileRTCPreProcessorDelegate @delegate);

		// -(MobileRTCRawDataError)setExternalVideoSource:(id<MobileRTCVideoSourceDelegate>)delegate;
		[Export ("setExternalVideoSource:")]
		MobileRTCRawDataError SetExternalVideoSource (MobileRTCVideoSourceDelegate @delegate);
	}

	// @interface MobileRTCShareSourceHelper : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCShareSourceHelper
	{
		// -(MobileRTCRawDataError)setExternalShareSource:(id<MobileRTCShareSourceDelegate> _Nullable)delegate __attribute__((deprecated("Use -setExternalShareSource:andAudioSource: instead")));
		[Export ("setExternalShareSource:")]
		MobileRTCRawDataError SetExternalShareSource ([NullAllowed] MobileRTCShareSourceDelegate @delegate);

		// -(MobileRTCRawDataError)setExternalShareSource:(id<MobileRTCShareSourceDelegate> _Nullable)shareDelegate andAudioSource:(id<MobileRTCShareAudioSourceDelegate> _Nullable)audioDelegate;
		[Export ("setExternalShareSource:andAudioSource:")]
		MobileRTCRawDataError SetExternalShareSource ([NullAllowed] MobileRTCShareSourceDelegate shareDelegate, [NullAllowed] MobileRTCShareAudioSourceDelegate audioDelegate);
	}

	// @interface MobileRTCAudioSourceHelper : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCAudioSourceHelper
	{
		// -(MobileRTCRawDataError)setExternalAudioSource:(id<MobileRTCAudioSourceDelegate> _Nullable)audioSourceDelegate;
		[Export ("setExternalAudioSource:")]
		MobileRTCRawDataError SetExternalAudioSource ([NullAllowed] MobileRTCAudioSourceDelegate audioSourceDelegate);
	}

	// @interface MobileRTCRealNameCountryInfo : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCRealNameCountryInfo
	{
		// @property (copy, nonatomic) NSString * _Nullable countryId;
		[NullAllowed, Export ("countryId")]
		string CountryId { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable countryName;
		[NullAllowed, Export ("countryName")]
		string CountryName { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable countryCode;
		[NullAllowed, Export ("countryCode")]
		string CountryCode { get; set; }
	}

	// @interface MobileRTCRetrieveSMSHandler : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCRetrieveSMSHandler
	{
		// -(BOOL)retrieve:(NSString * _Nullable)countryCode andPhoneNumber:(NSString * _Nullable)phoneNum;
		[Export ("retrieve:andPhoneNumber:")]
		bool Retrieve ([NullAllowed] string countryCode, [NullAllowed] string phoneNum);

		// -(BOOL)cancelAndLeaveMeeting;
		[Export ("cancelAndLeaveMeeting")]
		bool CancelAndLeaveMeeting { get; }
	}

	// @interface MobileRTCVerifySMSHandler : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCVerifySMSHandler
	{
		// -(BOOL)verify:(NSString * _Nullable)countryCode phoneNumber:(NSString * _Nullable)phoneNum andVerifyCode:(NSString * _Nullable)verifyCode;
		[Export ("verify:phoneNumber:andVerifyCode:")]
		bool Verify ([NullAllowed] string countryCode, [NullAllowed] string phoneNum, [NullAllowed] string verifyCode);

		// -(BOOL)cancelAndLeaveMeeting;
		[Export ("cancelAndLeaveMeeting")]
		bool CancelAndLeaveMeeting { get; }
	}

	// @interface MobileRTCSMSService : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCSMSService
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		MobileRTCSMSServiceDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<MobileRTCSMSServiceDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(void)enableZoomAuthRealNameMeetingUIShown:(_Bool)enable;
		[Export ("enableZoomAuthRealNameMeetingUIShown:")]
		void EnableZoomAuthRealNameMeetingUIShown (bool enable);

		// -(MobileRTCRetrieveSMSHandler * _Nullable)getResendSMSVerificationCodeHandler;
		[NullAllowed, Export ("getResendSMSVerificationCodeHandler")]
		MobileRTCRetrieveSMSHandler ResendSMSVerificationCodeHandler { get; }

		// -(MobileRTCVerifySMSHandler * _Nullable)getReVerifySMSVerificationCodeHandler;
		[NullAllowed, Export ("getReVerifySMSVerificationCodeHandler")]
		MobileRTCVerifySMSHandler ReVerifySMSVerificationCodeHandler { get; }

		// -(NSArray<MobileRTCRealNameCountryInfo *> * _Nullable)getSupportPhoneNumberCountryList;
		[NullAllowed, Export ("getSupportPhoneNumberCountryList")]
		MobileRTCRealNameCountryInfo[] SupportPhoneNumberCountryList { get; }

		// -(BOOL)setDefaultCellPhoneInfo:(NSString * _Nullable)countryCode phoneNum:(NSString * _Nullable)phoneNum;
		[Export ("setDefaultCellPhoneInfo:phoneNum:")]
		bool SetDefaultCellPhoneInfo ([NullAllowed] string countryCode, [NullAllowed] string phoneNum);
	}

	// @interface MobileRTCDirectShareViaMeetingIDOrPairingCodeHandler : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCDirectShareViaMeetingIDOrPairingCodeHandler
	{
		// -(BOOL)TryWithMeetingNumber:(NSString * _Nonnull)meetingNumber;
		[Export ("TryWithMeetingNumber:")]
		bool TryWithMeetingNumber (string meetingNumber);

		// -(BOOL)TryWithPairingCode:(NSString * _Nonnull)pairingCode;
		[Export ("TryWithPairingCode:")]
		bool TryWithPairingCode (string pairingCode);

		// -(BOOL)cancel;
		[Export ("cancel")]
		bool Cancel { get; }
	}

	// @protocol MobileRTCDirectShareServiceDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface MobileRTCDirectShareServiceDelegate
	{
		// @optional -(void)onDirectShareStatusUpdate:(MobileRTCDirectShareStatus)status handler:(MobileRTCDirectShareViaMeetingIDOrPairingCodeHandler * _Nullable)handler;
		[Export ("onDirectShareStatusUpdate:handler:")]
		void Handler (MobileRTCDirectShareStatus status, [NullAllowed] MobileRTCDirectShareViaMeetingIDOrPairingCodeHandler handler);
	}

	// @interface MobileRTCDirectShareService : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCDirectShareService
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		MobileRTCDirectShareServiceDelegate Delegate { get; set; }

		// @property (assign, nonatomic) id<MobileRTCDirectShareServiceDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Assign)]
		NSObject WeakDelegate { get; set; }

		// -(BOOL)canStartDirectShare;
		[Export ("canStartDirectShare")]
		bool CanStartDirectShare { get; }

		// -(BOOL)isDirectShareInProgress;
		[Export ("isDirectShareInProgress")]
		bool IsDirectShareInProgress { get; }

		// -(BOOL)startDirectShare;
		[Export ("startDirectShare")]
		bool StartDirectShare { get; }

		// -(BOOL)stopDirectShare;
		[Export ("stopDirectShare")]
		bool StopDirectShare { get; }
	}

	// @protocol MobileRTCReminderDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface MobileRTCReminderDelegate
	{
		// @optional -(void)onReminderNotify:(MobileRTCReminderContent * _Nullable)content handle:(MobileRTCReminderHandler * _Nullable)handler;
		[Export ("onReminderNotify:handle:")]
		void Handle ([NullAllowed] MobileRTCReminderContent content, [NullAllowed] MobileRTCReminderHandler handler);
	}

	// @interface MobileRTCReminderContent : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCReminderContent
	{
		// @property (assign, nonatomic) MobileRTCReminderType type;
		[Export ("type", ArgumentSemantic.Assign)]
		MobileRTCReminderType Type { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable title;
		[NullAllowed, Export ("title")]
		string Title { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable content;
		[NullAllowed, Export ("content")]
		string Content { get; set; }

		// @property (assign, nonatomic) BOOL isBlock;
		[Export ("isBlock")]
		bool IsBlock { get; set; }
	}

	// @interface MobileRTCReminderHandler : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCReminderHandler
	{
		// -(void)accept;
		[Export ("accept")]
		void Accept ();

		// -(void)declined;
		[Export ("declined")]
		void Declined ();

		// -(void)ignore;
		[Export ("ignore")]
		void Ignore ();
	}

	// @interface MobileRTCReminderHelper : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCReminderHelper
	{
		[Wrap ("WeakReminderDelegate")]
		[NullAllowed]
		MobileRTCReminderDelegate ReminderDelegate { get; set; }

		// @property (nonatomic, weak) id<MobileRTCReminderDelegate> _Nullable reminderDelegate;
		[NullAllowed, Export ("reminderDelegate", ArgumentSemantic.Weak)]
		NSObject WeakReminderDelegate { get; set; }
	}

	// @interface MobileRTCSDKInitContext : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTCSDKInitContext
	{
		// @property (copy, nonatomic) NSString * _Nullable domain;
		[NullAllowed, Export ("domain")]
		string Domain { get; set; }

		// @property (assign, nonatomic) BOOL enableLog;
		[Export ("enableLog")]
		bool EnableLog { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable bundleResPath;
		[NullAllowed, Export ("bundleResPath")]
		string BundleResPath { get; set; }

		// @property (assign, nonatomic) MobileRTC_ZoomLocale locale;
		[Export ("locale", ArgumentSemantic.Assign)]
		MobileRTC_ZoomLocale Locale { get; set; }

		// @property (assign, nonatomic) MobileRTCRawDataMemoryMode videoRawdataMemoryMode;
		[Export ("videoRawdataMemoryMode", ArgumentSemantic.Assign)]
		MobileRTCRawDataMemoryMode VideoRawdataMemoryMode { get; set; }

		// @property (assign, nonatomic) MobileRTCRawDataMemoryMode shareRawdataMemoryMode;
		[Export ("shareRawdataMemoryMode", ArgumentSemantic.Assign)]
		MobileRTCRawDataMemoryMode ShareRawdataMemoryMode { get; set; }

		// @property (assign, nonatomic) MobileRTCRawDataMemoryMode audioRawdataMemoryMode;
		[Export ("audioRawdataMemoryMode", ArgumentSemantic.Assign)]
		MobileRTCRawDataMemoryMode AudioRawdataMemoryMode { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable appGroupId;
		[NullAllowed, Export ("appGroupId")]
		string AppGroupId { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable replaykitBundleIdentifier;
		[NullAllowed, Export ("replaykitBundleIdentifier")]
		string ReplaykitBundleIdentifier { get; set; }

		// @property (assign, nonatomic) NSInteger wrapperType;
		[Export ("wrapperType")]
		nint WrapperType { get; set; }
	}

	// @interface MobileRTC : NSObject
	[BaseType (typeof(NSObject))]
	interface MobileRTC
	{
		// @property (readonly, retain, nonatomic) NSString * _Nullable mobileRTCDomain;
		[NullAllowed, Export ("mobileRTCDomain", ArgumentSemantic.Retain)]
		string MobileRTCDomain { get; }

		// @property (readonly, retain, nonatomic) NSString * _Nullable mobileRTCResPath;
		[NullAllowed, Export ("mobileRTCResPath", ArgumentSemantic.Retain)]
		string MobileRTCResPath { get; }

		// @property (readonly, retain, nonatomic) NSString * _Nullable mobileRTCCustomLocalizableName;
		[NullAllowed, Export ("mobileRTCCustomLocalizableName", ArgumentSemantic.Retain)]
		string MobileRTCCustomLocalizableName { get; }

		// +(MobileRTC * _Nonnull)sharedRTC;
		[Static]
		[Export ("sharedRTC")]
		MobileRTC SharedRTC { get; }

		// -(BOOL)initialize:(MobileRTCSDKInitContext * _Nonnull)context;
		[Export ("initialize:")]
		bool Initialize (MobileRTCSDKInitContext context);

		// -(BOOL)switchDomain:(NSString * _Nonnull)newDomain force:(BOOL)force;
		[Export ("switchDomain:force:")]
		bool SwitchDomain (string newDomain, bool force);

		// -(void)setMobileRTCCustomLocalizableName:(NSString * _Nullable)localizableName;
		[Export ("setMobileRTCCustomLocalizableName:")]
		void SetMobileRTCCustomLocalizableName ([NullAllowed] string localizableName);

		// -(UINavigationController * _Nullable)mobileRTCRootController;
		// -(void)setMobileRTCRootController:(UINavigationController * _Nullable)navController;
		[NullAllowed, Export ("mobileRTCRootController")]
		UINavigationController MobileRTCRootController { get; set; }

		// -(NSString * _Nullable)mobileRTCVersion;
		[Export ("mobileRTCVersion")]
		[return: NullAllowed]
		string MobileRTCVersion ();

		// -(BOOL)isRTCAuthorized;
		[Export ("isRTCAuthorized")]
		bool IsRTCAuthorized ();

		// -(BOOL)isSupportedCustomizeMeetingUI;
		[Export ("isSupportedCustomizeMeetingUI")]
		bool IsSupportedCustomizeMeetingUI ();

		// -(MobileRTCAuthService * _Nullable)getAuthService;
		[Export ("getAuthService")]
		[return: NullAllowed]
		MobileRTCAuthService GetAuthService ();

		// -(MobileRTCMeetingService * _Nullable)getMeetingService;
		[Export ("getMeetingService")]
		[return: NullAllowed]
		MobileRTCMeetingService GetMeetingService ();

		// -(MobileRTCMeetingSettings * _Nullable)getMeetingSettings;
		[Export ("getMeetingSettings")]
		[return: NullAllowed]
		MobileRTCMeetingSettings GetMeetingSettings ();

		// -(MobileRTCAnnotationService * _Nullable)getAnnotationService;
		[Export ("getAnnotationService")]
		[return: NullAllowed]
		MobileRTCAnnotationService GetAnnotationService ();

		// -(MobileRTCRemoteControlService * _Nullable)getRemoteControlService;
		[Export ("getRemoteControlService")]
		[return: NullAllowed]
		MobileRTCRemoteControlService GetRemoteControlService ();

		// -(MobileRTCWaitingRoomService * _Nullable)getWaitingRoomService;
		[Export ("getWaitingRoomService")]
		[return: NullAllowed]
		MobileRTCWaitingRoomService GetWaitingRoomService ();

		// -(MobileRTCSMSService * _Nullable)getSMSService;
		[Export ("getSMSService")]
		[return: NullAllowed]
		MobileRTCSMSService GetSMSService ();

		// -(MobileRTCDirectShareService * _Nullable)getDirectShareService;
		[Export ("getDirectShareService")]
		[return: NullAllowed]
		MobileRTCDirectShareService GetDirectShareService ();

		// -(MobileRTCReminderHelper * _Nullable)getReminderHelper;
		[Export ("getReminderHelper")]
		[return: NullAllowed]
		MobileRTCReminderHelper GetReminderHelper ();

		// -(MobileRTCVideoSourceHelper * _Nullable)getVideoSourceHelper;
		[Export ("getVideoSourceHelper")]
		[return: NullAllowed]
		MobileRTCVideoSourceHelper GetVideoSourceHelper ();

		// -(MobileRTCShareSourceHelper * _Nullable)getShareSourceHelper;
		[Export ("getShareSourceHelper")]
		[return: NullAllowed]
		MobileRTCShareSourceHelper GetShareSourceHelper ();

		// -(NSArray<NSString *> * _Nonnull)supportedLanguages;
		[Export ("supportedLanguages")]
		string[] SupportedLanguages ();

		// -(void)setLanguage:(NSString * _Nullable)lang;
		[Export ("setLanguage:")]
		void SetLanguage ([NullAllowed] string lang);

		// -(void)appWillResignActive;
		[Export ("appWillResignActive")]
		void AppWillResignActive ();

		// -(void)appDidBecomeActive;
		[Export ("appDidBecomeActive")]
		void AppDidBecomeActive ();

		// -(void)appDidEnterBackgroud;
		[Export ("appDidEnterBackgroud")]
		void AppDidEnterBackgroud ();

		// -(void)appWillTerminate;
		[Export ("appWillTerminate")]
		void AppWillTerminate ();

		// -(void)cleanup;
		[Export ("cleanup")]
		void Cleanup ();

		//// -(void)willTransitionToTraitCollection:(UITraitCollection * _Nullable)newCollection withTransitionCoordinator:(id<UIViewControllerTransitionCoordinator> _Nullable)coordinator;
		//[Export ("willTransitionToTraitCollection:withTransitionCoordinator:")]
		//void WillTransitionToTraitCollection ([NullAllowed] UITraitCollection newCollection, [NullAllowed] UIViewControllerTransitionCoordinator coordinator);

		//// -(void)viewWillTransitionToSize:(CGSize)size withTransitionCoordinator:(id<UIViewControllerTransitionCoordinator> _Nullable)coordinator;
		//[Export ("viewWillTransitionToSize:withTransitionCoordinator:")]
		//void ViewWillTransitionToSize (CGSize size, [NullAllowed] UIViewControllerTransitionCoordinator coordinator);

		// -(BOOL)hasRawDataLicense;
		[Export ("hasRawDataLicense")]
		bool HasRawDataLicense ();
    }

    // @interface MobileRTCInMeetingDeviceInfo : NSObject
    [BaseType(typeof(NSObject))]
    interface MobileRTCInMeetingDeviceInfo
    {
        // @property (readonly, assign, nonatomic) NSInteger index;
        [Export("index")]
        nint Index { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable deviceName;
        [NullAllowed, Export("deviceName")]
        string DeviceName { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable meetingTopic;
        [NullAllowed, Export("meetingTopic")]
        string MeetingTopic { get; }

        // @property (readonly, assign, nonatomic) NSUInteger meetingNumber;
        [Export("meetingNumber")]
        nuint MeetingNumber { get; }
    }

    // @interface MobileRTCContactInfo : NSObject
    [BaseType(typeof(NSObject))]
    interface MobileRTCContactInfo
    {
        // @property (readonly, copy, nonatomic) NSString * _Nullable contactID;
        [NullAllowed, Export("contactID")]
        string ContactID { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable contactName;
        [NullAllowed, Export("contactName")]
        string ContactName { get; }

        // @property (readonly, assign, nonatomic) MobileRTCPresenceStatus presenceStatus;
        [Export("presenceStatus", ArgumentSemantic.Assign)]
        MobileRTCPresenceStatus PresenceStatus { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable profilepicture;
        [NullAllowed, Export("profilepicture")]
        string Profilepicture { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable personalNote;
        [NullAllowed, Export("personalNote")]
        string PersonalNote { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable companyName;
        [NullAllowed, Export("companyName")]
        string CompanyName { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable department;
        [NullAllowed, Export("department")]
        string Department { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable jobTitle;
        [NullAllowed, Export("jobTitle")]
        string JobTitle { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable phoneNumber;
        [NullAllowed, Export("phoneNumber")]
        string PhoneNumber { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable email;
        [NullAllowed, Export("email")]
        string Email { get; }
    }

    // @interface MobileRTCInvitationMeetingHandler : NSObject
    [BaseType(typeof(NSObject))]
    interface MobileRTCInvitationMeetingHandler
    {
        // @property (readonly, copy, nonatomic) NSString * _Nullable senderName;
        [NullAllowed, Export("senderName")]
        string SenderName { get; }

        // @property (readonly, assign, nonatomic) long long meetingNumber;
        [Export("meetingNumber")]
        long MeetingNumber { get; }

        // @property (readonly, assign, nonatomic) BOOL isChannelInvitation;
        [Export("isChannelInvitation")]
        bool IsChannelInvitation { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable channelName;
        [NullAllowed, Export("channelName")]
        string ChannelName { get; }

        // @property (readonly, assign, nonatomic) unsigned int channelMemberCount;
        [Export("channelMemberCount")]
        uint ChannelMemberCount { get; }

        // -(void)setScreenName:(NSString * _Nullable)screenName;
        [Export("setScreenName:")]
        void SetScreenName([NullAllowed] string screenName);

        // -(MobileRTCSDKError)accept;
        [Export("accept")]
        MobileRTCSDKError Accept { get; }

        // -(MobileRTCSDKError)decline;
        [Export("decline")]
        MobileRTCSDKError Decline { get; }

        // -(MobileRTCSDKError)timeout;
        [Export("timeout")]
        MobileRTCSDKError Timeout { get; }
    }

    // @protocol MobileRTCPresenceHelperDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MobileRTCPresenceHelperDelegate
    {
        // @required -(void)onRequestStarContact:(NSArray<NSString *> * _Nullable)contactIDList;
        [Abstract]
        [Export("onRequestStarContact:")]
        void OnRequestStarContact([NullAllowed] string[] contactIDList);

        // @required -(void)onRequestContactDetailInfo:(NSArray<MobileRTCContactInfo *> * _Nullable)contactList;
        [Abstract]
        [Export("onRequestContactDetailInfo:")]
        void OnRequestContactDetailInfo([NullAllowed] MobileRTCContactInfo[] contactList);

        // @required -(void)onUserPresenceChanged:(NSString * _Nullable)contactID presenceStatus:(MobileRTCPresenceStatus)status;
        [Abstract]
        [Export("onUserPresenceChanged:presenceStatus:")]
        void OnUserPresenceChanged([NullAllowed] string contactID, MobileRTCPresenceStatus status);

        // @required -(void)onStarContactListChanged:(NSArray<NSString *> * _Nullable)contactIDList isAdd:(BOOL)add;
        [Abstract]
        [Export("onStarContactListChanged:isAdd:")]
        void OnStarContactListChanged([NullAllowed] string[] contactIDList, bool add);

        // @required -(void)onReceiveInvitationToMeeting:(MobileRTCInvitationMeetingHandler * _Nullable)handler;
        [Abstract]
        [Export("onReceiveInvitationToMeeting:")]
        void OnReceiveInvitationToMeeting([NullAllowed] MobileRTCInvitationMeetingHandler handler);

        // @required -(void)onMeetingInvitationCanceled:(long long)meetingNumber;
        [Abstract]
        [Export("onMeetingInvitationCanceled:")]
        void OnMeetingInvitationCanceled(long meetingNumber);

        // @required -(void)onMeetingAcceptedByOtherDevice:(long long)meetingNumber;
        [Abstract]
        [Export("onMeetingAcceptedByOtherDevice:")]
        void OnMeetingAcceptedByOtherDevice(long meetingNumber);

        // @required -(void)onMeetingInvitationDeclined:(NSString * _Nullable)contactID;
        [Abstract]
        [Export("onMeetingInvitationDeclined:")]
        void OnMeetingInvitationDeclined([NullAllowed] string contactID);

        // @required -(void)onMeetingDeclinedByOtherDevice:(long long)meetingNumber;
        [Abstract]
        [Export("onMeetingDeclinedByOtherDevice:")]
        void OnMeetingDeclinedByOtherDevice(long meetingNumber);
    }

    // @interface MobileRTCPresenceHelper : NSObject
    [BaseType(typeof(NSObject))]
    interface MobileRTCPresenceHelper
    {
        [Wrap("WeakDelegate")]
        [NullAllowed]
        MobileRTCPresenceHelperDelegate Delegate { get; set; }

        // @property (assign, nonatomic) id<MobileRTCPresenceHelperDelegate> _Nullable delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Assign)]
        NSObject WeakDelegate { get; set; }

        // -(MobileRTCSDKError)requestStarContact;
        [Export("requestStarContact")]
        MobileRTCSDKError RequestStarContact { get; }

        // -(MobileRTCSDKError)starContact:(NSString * _Nonnull)contactID;
        [Export("starContact:")]
        MobileRTCSDKError StarContact(string contactID);

        // -(MobileRTCSDKError)unStarContact:(NSString * _Nonnull)contactID;
        [Export("unStarContact:")]
        MobileRTCSDKError UnStarContact(string contactID);

        // -(MobileRTCSDKError)inviteContact:(NSString * _Nonnull)contactID;
        [Export("inviteContact:")]
        MobileRTCSDKError InviteContact(string contactID);

        // -(MobileRTCSDKError)inviteContactList:(NSArray<NSString *> * _Nonnull)contactIDList;
        [Export("inviteContactList:")]
        MobileRTCSDKError InviteContactList(string[] contactIDList);

        // -(MobileRTCSDKError)requestContactDetailInfo:(NSArray<NSString *> * _Nonnull)contactIDList;
        [Export("requestContactDetailInfo:")]
        MobileRTCSDKError RequestContactDetailInfo(string[] contactIDList);

        // -(MobileRTCSDKError)subscribeContactPresence:(NSArray<NSString *> * _Nonnull)contactIDList;
        [Export("subscribeContactPresence:")]
        MobileRTCSDKError SubscribeContactPresence(string[] contactIDList);

        // -(MobileRTCSDKError)unSubscribeContactPresence:(NSArray<NSString *> * _Nonnull)contactIDList;
        [Export("unSubscribeContactPresence:")]
        MobileRTCSDKError UnSubscribeContactPresence(string[] contactIDList);
    }

    // @protocol MobileRTCNotificationServiceHelperDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MobileRTCNotificationServiceHelperDelegate
    {
        // @optional -(void)onMeetingDeviceListChanged:(NSArray<MobileRTCInMeetingDeviceInfo *> * _Nullable)deviceList;
        [Export("onMeetingDeviceListChanged:")]
        void OnMeetingDeviceListChanged([NullAllowed] MobileRTCInMeetingDeviceInfo[] deviceList);

        // @optional -(void)onTransferMeetingStatus:(BOOL)bSuccess;
        [Export("onTransferMeetingStatus:")]
        void OnTransferMeetingStatus(bool bSuccess);
    }

    // @interface MobileRTCNotificationServiceHelper : NSObject
    [BaseType(typeof(NSObject))]
    interface MobileRTCNotificationServiceHelper
    {
        [Wrap("WeakDelegate")]
        [NullAllowed]
        MobileRTCNotificationServiceHelperDelegate Delegate { get; set; }

        // @property (assign, nonatomic) id<MobileRTCNotificationServiceHelperDelegate> _Nullable delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Assign)]
        NSObject WeakDelegate { get; set; }

        // -(BOOL)isTransferMeetingEnabled;
        [Export("isTransferMeetingEnabled")]
        bool IsTransferMeetingEnabled { get; }

        // -(BOOL)isPresenceFeatureEnabled;
        [Export("isPresenceFeatureEnabled")]
        bool IsPresenceFeatureEnabled { get; }

        // -(MobileRTCSDKError)transferMeeting:(NSInteger)index;
        [Export("transferMeeting:")]
        MobileRTCSDKError TransferMeeting(nint index);

        // -(MobileRTCPresenceHelper * _Nullable)getPresenceHelper;
        [NullAllowed, Export("getPresenceHelper")]
        MobileRTCPresenceHelper PresenceHelper { get; }
    }
}